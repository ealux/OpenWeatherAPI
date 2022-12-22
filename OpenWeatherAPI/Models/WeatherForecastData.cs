using OpenWeatherAPI.Converters;
using System;
using System.Text.Json.Serialization;

namespace OpenWeatherAPI.Models
{
    public class WeatherForecastData
    {
        [JsonPropertyName("cnt")] public int Count { get; set; }
        [JsonPropertyName("list")] public ForecastData[] Data { get; set; }
        [JsonPropertyName("city")] public City City { get; set; }
    }
}