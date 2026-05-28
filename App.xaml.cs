using System;
using System.Threading;
using System.Windows;
using System.Drawing;

namespace TimeZoner;

public partial class App : Application
{
    private static Mutex? _mutex = null;
    private System.Windows.Forms.NotifyIcon? _notifyIcon;

    protected override void OnStartup(StartupEventArgs e)
    {
        const string appName = "TimeZoner_Unique_App_Mutex";
        bool createdNew;

        _mutex = new Mutex(true, appName, out createdNew);

        if (!createdNew)
        {
            // App is already running, exit immediately
            Application.Current.Shutdown();
            return;
        }

        // Setup System Tray Icon
        _notifyIcon = new System.Windows.Forms.NotifyIcon();
        // Fallback to a system icon since we don't have a custom .ico
        _notifyIcon.Icon = SystemIcons.Application;
        _notifyIcon.Visible = true;
        _notifyIcon.Text = "TimeZoner";

        var contextMenu = new System.Windows.Forms.ContextMenuStrip();
        
        var settingsItem = new System.Windows.Forms.ToolStripMenuItem("Settings");
        settingsItem.Click += (s, args) => 
        {
            // Ensure Settings window is shown
            var settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        };
        
        var exitItem = new System.Windows.Forms.ToolStripMenuItem("Exit");
        exitItem.Click += (s, args) => Application.Current.Shutdown();

        contextMenu.Items.Add(settingsItem);
        contextMenu.Items.Add(new System.Windows.Forms.ToolStripSeparator());
        contextMenu.Items.Add(exitItem);

        _notifyIcon.ContextMenuStrip = contextMenu;

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        if (_notifyIcon != null)
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }
        base.OnExit(e);
    }
}
