using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MetaWeather
{
    public class WeatherLocation
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("local_names")]
        public Dictionary<string, string> LocalNames { get; set; }
    }
}