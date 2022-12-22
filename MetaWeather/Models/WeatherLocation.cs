using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Weather
{
    public class WeatherLocation
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string CountryCode { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("local_names")]
        public Dictionary<string, string> LocalNames { get; set; }

        /// <summary> Text interpretation </summary>
        public override string ToString() => $"{Name} - [{CountryCode}][{State}]: {(Latitude, Longitude)}";
    }
}