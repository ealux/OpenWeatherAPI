using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherAPI.Converters
{
    internal class JsonIntToDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var datetime = new DateTime(reader.GetInt64());
            return datetime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) => 
            writer.WriteStringValue(value.Ticks.ToString());
    }
}

