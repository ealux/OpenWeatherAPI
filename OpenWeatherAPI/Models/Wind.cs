using System.Text.Json.Serialization;

namespace OpenWeatherAPI.Models
{
    public class Wind
    {
        [JsonPropertyName("speed")] public double Speed { get; set; }
        [JsonPropertyName("deg")] public int Degree { get; set; }
        [JsonPropertyName("gust")] public double Gust { get; set; }
    }
}