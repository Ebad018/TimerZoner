using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using TimeZoner.Services;

namespace TimeZoner.ViewModels;

public partial class CityViewModel : ObservableObject
{
    [ObservableProperty]
    private string name = "";

    [ObservableProperty]
    private string currentTime = "";

    [ObservableProperty]
    private string currentDate = "";

    [ObservableProperty]
    private string temperatureText = "--°";

    [ObservableProperty]
    private string weatherIcon = "⌛";

    private TimeZoneInfo timeZone;
    private bool is24HourFormat;
    private CityConfig config;

    public CityViewModel(CityConfig config, bool is24Hour)
    {
        this.config = config;
        this.Name = config.Name;
        this.is24HourFormat = is24Hour;
        try 
        {
            this.timeZone = TimeZoneInfo.FindSystemTimeZoneById(config.TimeZoneId);
        }
        catch
        {
            this.timeZone = TimeZoneInfo.Local;
        }
        UpdateTime();
        LoadWeatherAsync();
    }

    public void UpdateTime()
    {
        var time = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
        CurrentTime = is24HourFormat ? time.ToString("HH:mm") : time.ToString("hh:mm tt");
        CurrentDate = time.ToString("ddd, MMM dd");
    }

    private async void LoadWeatherAsync()
    {
        var weather = await WeatherService.GetWeatherAsync(config.Latitude, config.Longitude);
        if (weather != null)
        {
            TemperatureText = $"{Math.Round(weather.Temperature)}°C";
            WeatherIcon = GetIcon(weather.WeatherCode, weather.IsDay);
        }
        else
        {
            WeatherIcon = "🚫"; // Offline or Error
        }
    }

    private string GetIcon(int code, bool isDay)
    {
        if (code == 0) return isDay ? "☀️" : "🌙";
        if (code >= 1 && code <= 3) return "☁️";
        if (code >= 45 && code <= 48) return "🌫️";
        if (code >= 51 && code <= 65) return "🌧️";
        if (code >= 71 && code <= 75) return "❄️";
        if (code >= 95) return "⛈️";
        return isDay ? "⛅" : "☁️";
    }
}

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<CityViewModel> cities = new();

    [ObservableProperty]
    private string themeColor = "#FFFFFFFF";

    private DispatcherTimer timer;
    private AppSettings settings;

    public MainViewModel()
    {
        settings = SettingsManager.LoadSettings();
        ThemeColor = settings.ThemeColor;

        // If no cities, add default
        if (settings.Cities.Count == 0)
        {
            settings.Cities.Add(new CityConfig { Name = "Local Time", TimeZoneId = TimeZoneInfo.Local.Id, Latitude = 0, Longitude = 0 });
            // Add some examples for testing
            settings.Cities.Add(new CityConfig { Name = "London", TimeZoneId = "GMT Standard Time", Latitude = 51.5074, Longitude = -0.1278 });
            settings.Cities.Add(new CityConfig { Name = "Tokyo", TimeZoneId = "Tokyo Standard Time", Latitude = 35.6762, Longitude = 139.6503 });
            SettingsManager.SaveSettings(settings);
        }

        foreach (var city in settings.Cities)
        {
            Cities.Add(new CityViewModel(city, settings.Is24HourFormat));
        }

        timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        timer.Tick += (s, e) =>
        {
            foreach (var city in Cities)
            {
                city.UpdateTime();
            }
        };
        timer.Start();
    }
}
