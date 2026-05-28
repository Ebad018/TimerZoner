using Microsoft.Win32;
using System;
using System.IO;

namespace TimeZoner.Interop;

public static class StartupManager
{
    public static void SetAutoStart(bool enable)
    {
        try
        {
            using RegistryKey? rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rk != null)
            {
                string appName = "TimeZonerWidget";
                string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                
                // If running as dll (like in .NET 5+), find the exe
                if (appPath.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                {
                    appPath = appPath.Substring(0, appPath.Length - 4) + ".exe";
                }

                if (enable)
                {
                    rk.SetValue(appName, $"\"{appPath}\"");
                }
                else
                {
                    rk.DeleteValue(appName, false);
                }
            }
        }
        catch
        {
            // Ignore registry errors for now
        }
    }
}
