using OpenWeatherAPI.Converters;
using System;
using System.Text.Json.Serialization;

namespace OpenWeatherAPI.Models
{
    public class ForecastData
    {
        [JsonPropertyName("main")] public WeatherInfo Weather { get; set; }
        [JsonPropertyName("clouds")] public Clouds Clouds { get; set; }
        [JsonPropertyName("wind")] public Wind Wind { get; set; }

        /// <summary> Average visibility, metres. The maximum value of the visibility is 10km </summary>
        [JsonPropertyName("visibility")] public int Visibility { get; set; }

        /// <summary> Probability of precipitation.
        /// The values of the parameter vary between 0 and 1, where 0 is equal to 0%, 1 is equal to 100%
        /// </summary>
        [JsonPropertyName("pop")] public double Precipitation { get; set; }

        /// <summary> Time of data forecasted, unix, UTC </summary>
        [JsonPropertyName("dt_txt")]
        [JsonConverter(typeof(JsonStringToDateTimeConverter))]
        public DateTime TimestampUTC { get; set; }
    }
}