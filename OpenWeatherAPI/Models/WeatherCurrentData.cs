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
}