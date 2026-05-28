using System.Collections.Generic;
using System.Linq;

namespace TimeZoner.Models;

public static class CityDatabase
{
    public static List<Services.CityConfig> AllCities { get; } = new List<Services.CityConfig>
    {
        // Requested Cities
        new() { Name = "Islamabad, Pakistan", TimeZoneId = "Pakistan Standard Time", Latitude = 33.6844, Longitude = 73.0479 },
        new() { Name = "Stockholm, Sweden", TimeZoneId = "W. Europe Standard Time", Latitude = 59.3293, Longitude = 18.0686 },
        new() { Name = "Leeds, UK", TimeZoneId = "GMT Standard Time", Latitude = 53.8008, Longitude = -1.5491 },
        new() { Name = "Bournemouth, UK", TimeZoneId = "GMT Standard Time", Latitude = 50.7192, Longitude = -1.8795 },
        new() { Name = "Istanbul, Turkiye", TimeZoneId = "Turkey Standard Time", Latitude = 41.0082, Longitude = 28.9784 },
        new() { Name = "Washington D.C., USA", TimeZoneId = "Eastern Standard Time", Latitude = 38.9072, Longitude = -77.0369 },
        new() { Name = "Virginia (Richmond), USA", TimeZoneId = "Eastern Standard Time", Latitude = 37.5407, Longitude = -77.4360 },
        new() { Name = "Beijing, China", TimeZoneId = "China Standard Time", Latitude = 39.9042, Longitude = 116.4074 },
        new() { Name = "Zurich, Switzerland", TimeZoneId = "W. Europe Standard Time", Latitude = 47.3769, Longitude = 8.5417 },
        
        // Major Global Cities
        new() { Name = "New York, USA", TimeZoneId = "Eastern Standard Time", Latitude = 40.7128, Longitude = -74.0060 },
        new() { Name = "Los Angeles, USA", TimeZoneId = "Pacific Standard Time", Latitude = 34.0522, Longitude = -118.2437 },
        new() { Name = "London, UK", TimeZoneId = "GMT Standard Time", Latitude = 51.5074, Longitude = -0.1278 },
        new() { Name = "Tokyo, Japan", TimeZoneId = "Tokyo Standard Time", Latitude = 35.6762, Longitude = 139.6503 },
        new() { Name = "Sydney, Australia", TimeZoneId = "AUS Eastern Standard Time", Latitude = -33.8688, Longitude = 151.2093 },
        new() { Name = "Paris, France", TimeZoneId = "Romance Standard Time", Latitude = 48.8566, Longitude = 2.3522 },
        new() { Name = "Berlin, Germany", TimeZoneId = "W. Europe Standard Time", Latitude = 52.5200, Longitude = 13.4050 },
        new() { Name = "Dubai, UAE", TimeZoneId = "Arabian Standard Time", Latitude = 25.2048, Longitude = 55.2708 },
        new() { Name = "Singapore", TimeZoneId = "Singapore Standard Time", Latitude = 1.3521, Longitude = 103.8198 },
        new() { Name = "Seoul, South Korea", TimeZoneId = "Korea Standard Time", Latitude = 37.5665, Longitude = 126.9780 },
        new() { Name = "Toronto, Canada", TimeZoneId = "Eastern Standard Time", Latitude = 43.6532, Longitude = -79.3832 },
        new() { Name = "Moscow, Russia", TimeZoneId = "Russian Standard Time", Latitude = 55.7558, Longitude = 37.6173 },
        new() { Name = "Mumbai, India", TimeZoneId = "India Standard Time", Latitude = 19.0760, Longitude = 72.8777 },
        new() { Name = "Sao Paulo, Brazil", TimeZoneId = "E. South America Standard Time", Latitude = -23.5505, Longitude = -46.6333 },
        new() { Name = "Cairo, Egypt", TimeZoneId = "Egypt Standard Time", Latitude = 30.0444, Longitude = 31.2357 },
        new() { Name = "Mexico City, Mexico", TimeZoneId = "Central Standard Time (Mexico)", Latitude = 19.4326, Longitude = -99.1332 },
        new() { Name = "Bangkok, Thailand", TimeZoneId = "SE Asia Standard Time", Latitude = 13.7563, Longitude = 100.5018 },
        new() { Name = "Riyadh, Saudi Arabia", TimeZoneId = "Arab Standard Time", Latitude = 24.7136, Longitude = 46.6753 }
    };

    public static List<Services.CityConfig> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return AllCities;
            
        return AllCities
            .Where(c => c.Name.Contains(query, System.StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
