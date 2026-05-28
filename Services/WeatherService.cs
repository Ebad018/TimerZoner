using System.Net.Http;
using System.Text.Json;

namespace TimeZoner.Services;

public class WeatherInfo
{
    public double Temperature { get; set; }
    public int WeatherCode { get; set; }
    public bool IsDay { get; set; }
    public string ConditionDescription => GetCondition(WeatherCode);

    private string GetCondition(int code)
    {
        return code switch
        {
            0 => "Clear sky",
            1 or 2 or 3 => "Cloudy",
            45 or 48 => "Fog",
            51 or 53 or 55 => "Drizzle",
            61 or 63 or 65 => "Rain",
            71 or 73 or 75 => "Snow",
            95 or 96 or 99 => "Thunderstorm",
            _ => "Unknown"
        };
    }
}

public static class WeatherService
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task<WeatherInfo?> GetWeatherAsync(double lat, double lon)
    {
        if (lat == 0 && lon == 0) return null; // No coordinates

        try
        {
            // Open-Meteo API
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current=temperature_2m,is_day,weather_code";
            var response = await client.GetStringAsync(url);
            
            using var doc = JsonDocument.Parse(response);
            var current = doc.RootElement.GetProperty("current");

            return new WeatherInfo
            {
                Temperature = current.GetProperty("temperature_2m").GetDouble(),
                IsDay = current.GetProperty("is_day").GetInt32() == 1,
                WeatherCode = current.GetProperty("weather_code").GetInt32()
            };
        }
        catch
        {
            // Fail gracefully if offline
            return null;
        }
    }
}
