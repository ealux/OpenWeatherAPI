using OpenWeatherAPI.Converters;
using System;
using System.Text.Json.Serialization;

namespace OpenWeatherAPI.Models
{
    public class WeatherCurrentData
    {
        public City City { get; set; }

        public WeatherInfo Weather { get; set; }

        public Clouds Clouds { get; set; }

        public Wind Wind { get; set; }

        /// <summary> Average visibility, metres. The maximum value of the visibility is 10km </summary>
        public int Visibility { get; set; }

        public DateTime TimestampUTC { get; set; }
    }

    internal class WeatherCurrentDataInternal
    {
        [JsonPropertyName("coord")] public Location Location { get; set; }
        [JsonPropertyName("main")] public WeatherInfo Weather { get; set; }

        /// <summary> Average visibility, metres. The maximum value of the visibility is 10km </summary>
        [JsonPropertyName("visibility")] public int Visibility { get; set; }

        [JsonPropertyName("wind")] public Wind Wind { get; set; }
        [JsonPropertyName("clouds")] public Clouds Clouds { get; set; }

        [JsonPropertyName("dt")]
        [JsonConverter(typeof(JsonInt64ToDateTimeConverter))]
        public DateTime TimestampUTC { get; set; }

        [JsonPropertyName("sys")] public City City { get; set; }

        [JsonPropertyName("timezone")]
        [JsonConverter(typeof(JsonInt64ToDateTimeConverter))]
        public DateTime TimezoneFromUTC { get; set; }

        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
    }
}