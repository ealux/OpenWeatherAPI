using System;
using System.Text.Json.Serialization;
using WeatherAPI.Converters;

namespace MetaWeather.Models
{
    public class WeatherData
    {
        [JsonPropertyName("cnt")] public int Count { get; set; }
        [JsonPropertyName("list")] public Data[] Data { get; set; }
        [JsonPropertyName("city")] public City City { get; set; }

        
    }

    public class City
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("coord")] public Location Location { get; set; }
        [JsonPropertyName("country")] public string Country { get; set; }
        [JsonPropertyName("population")] public int Population { get; set; }
        [JsonPropertyName("timezone")] public int Timezone { get; set; }
        [JsonPropertyName("sunrise")] public int SunRise { get; set; }
        [JsonPropertyName("sunset")] public int SunSet { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("lat")] public double Latitude { get; set; }
        [JsonPropertyName("lon")] public double Longitude { get; set; }
    }

    public class Data
    {
        /// <summary> Time of data forecasted, unix, UTC </summary>
        [JsonPropertyName("dt")] public int Timestamp { get; set; }

        [JsonPropertyName("main")] public Main main { get; set; }

        [JsonPropertyName("clouds")] public Clouds Clouds { get; set; }
        [JsonPropertyName("wind")] public Wind Wind { get; set; }

        /// <summary> Average visibility, metres. The maximum value of the visibility is 10km </summary>
        [JsonPropertyName("visibility")] public int Visibility { get; set; }

        /// <summary> Probability of precipitation.
        /// The values of the parameter vary between 0 and 1, where 0 is equal to 0%, 1 is equal to 100% 
        /// </summary>
        [JsonPropertyName("pop")] public double Precipitation { get; set; }
        [JsonPropertyName("dt_txt")] public string Timestamp_string { get; set; }
    }

    public class Main
    {
        [JsonPropertyName("temp")] public double CurrentTemp { get; set; }
        [JsonPropertyName("feels_like")] public double FeelsLikeTemp { get; set; }
        [JsonPropertyName("temp_min")] public double MinTemp { get; set; }
        [JsonPropertyName("temp_max")] public double MaxTemp { get; set; }
        [JsonPropertyName("pressure")] public int Pressure { get; set; }
        [JsonPropertyName("sea_level")] public int SeaLevel { get; set; }
        [JsonPropertyName("grnd_level")] public int GroundLevel { get; set; }
        [JsonPropertyName("humidity")] public int Humidity { get; set; }
        [JsonPropertyName("temp_kf")] public double KoefTemp { get; set; }
    }

    public class Clouds
    {
        /// <summary> Cloudiness, [%] </summary>
        [JsonPropertyName("all")] public int Percentage { get; set; }
    }

    public class Wind
    {
        [JsonPropertyName("speed")] public double Speed { get; set; }
        [JsonPropertyName("deg")] public int Degree { get; set; }
        [JsonPropertyName("gust")] public double Gust { get; set; }
    }
}