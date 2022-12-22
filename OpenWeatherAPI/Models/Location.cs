using System.Text.Json.Serialization;

namespace OpenWeatherAPI.Models
{
    public class Location
    {
        [JsonPropertyName("lat")] public double Latitude { get; set; }
        [JsonPropertyName("lon")] public double Longitude { get; set; }
    }
}