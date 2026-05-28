using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;

namespace TimeZoner.Services;

public class AppSettings
{
    public ObservableCollection<CityConfig> Cities { get; set; } = new ObservableCollection<CityConfig>();
    public bool Is24HourFormat { get; set; } = true;
    public string ThemeColor { get; set; } = "#FFFFFFFF"; // Default white
    public bool AutoStart { get; set; } = true;
}

public class CityConfig
{
    public string Name { get; set; } = string.Empty;
    public string TimeZoneId { get; set; } = string.Empty; // e.g., "Turkey Standard Time"
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public static class SettingsManager
{
    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
        "TimeZoner", 
        "settings.json");

    public static AppSettings LoadSettings()
    {
        if (File.Exists(SettingsPath))
        {
            var json = File.ReadAllText(SettingsPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(json);
            return settings ?? GetDefaultSettings();
        }
        return GetDefaultSettings();
    }

    public static void SaveSettings(AppSettings settings)
    {
        var dir = Path.GetDirectoryName(SettingsPath);
        if (dir != null && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SettingsPath, json);
    }

    private static AppSettings GetDefaultSettings()
    {
        return new AppSettings
        {
            Cities = new ObservableCollection<CityConfig>
            {
                new CityConfig { Name = "Local", TimeZoneId = TimeZoneInfo.Local.Id, Latitude = 0, Longitude = 0 }
            }
        };
    }
}
