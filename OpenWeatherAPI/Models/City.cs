using OpenWeatherAPI.Converters;
using System;
using System.Text.Json.Serialization;

namespace OpenWeatherAPI.Models
{
    public class City
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("coord")] public Location Location { get; set; }
        [JsonPropertyName("country")] public string Country { get; set; }
        [JsonPropertyName("population")] public int Population { get; set; }

        [JsonPropertyName("timezone")]
        [JsonConverter(typeof(JsonInt64ToDateTimeConverter))]
        public DateTime TimezoneFromUTC { get; set; }

        [JsonPropertyName("sunrise")]
        [JsonConverter(typeof(JsonInt64ToDateTimeConverter))]
        public DateTime SunRiseUTC { get; set; }

        [JsonPropertyName("sunset")]
        [JsonConverter(typeof(JsonInt64ToDateTimeConverter))]
        public DateTime SunSetUTC { get; set; }
    }
}