using System.Text.Json.Serialization;

namespace MetaWeather
{
    public enum LocalNames
    {
        [JsonPropertyName("feature_name")]
        feature_name,

        [JsonPropertyName("ascii")]
        ascii,

        [JsonPropertyName("ru")]
        ru
    }
}