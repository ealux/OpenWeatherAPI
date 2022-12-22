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

    //internal class JsonLocalNamesConverter : JsonConverter<LocalNames[]>
    //{
    //    public override LocalNames[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        var dict = 
    //    }

    //    public override void Write(Utf8JsonWriter writer, LocalNames[] value, JsonSerializerOptions options)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}