using System.Text.Json.Serialization;

namespace OpenWeatherAPI.Models
{
    public class Clouds
    {
        /// <summary> Cloudiness, [%] </summary>
        [JsonPropertyName("all")] public int Percentage { get; set; }
    }
}