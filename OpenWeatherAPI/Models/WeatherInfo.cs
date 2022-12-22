using System.Text.Json.Serialization;

namespace OpenWeatherAPI.Models
{
    public class WeatherInfo
    {
        [JsonPropertyName("temp")] public double CurrentTemp { get; set; }
        [JsonPropertyName("feels_like")] public double FeelsLikeTemp { get; set; }
        [JsonPropertyName("temp_min")] public double MinTemp { get; set; }
        [JsonPropertyName("temp_max")] public double MaxTemp { get; set; }
        [JsonPropertyName("pressure")] public int Pressure { get; set; }
        [JsonPropertyName("sea_level")] public int SeaLevel { get; set; }
        [JsonPropertyName("grnd_level")] public int GroundLevel { get; set; }
        [JsonPropertyName("humidity")] public int Humidity { get; set; }
    }
}