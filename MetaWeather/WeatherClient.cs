using MetaWeather.Models;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MetaWeather
{
    public class WeatherClient
    {
        private const string _ApiKey = "c082f7450a91dda8d92fff5710d75da5";
        private readonly HttpClient _client;

        private static readonly JsonSerializerOptions __JsonOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

        public WeatherClient(HttpClient client) => this._client = client;

        #region [Location]

        /// <summary> Get Location (city) by name </summary>
        public async Task<WeatherLocation[]> GetLocation(
            string name,
            int limit = 1,
            CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<WeatherLocation[]>(
                $"/geo/1.0/direct?q={name}&" +
                (limit <= 0 ? $"limit={int.MaxValue}&" : $"limit={limit}&") +
                $"appid={_ApiKey}",
                __JsonOptions,
                cancel)
                .ConfigureAwait(false);

        /// <summary> Get Location (city) by coordinates </summary>
        public async Task<WeatherLocation[]> GetLocation(
            (double Latitude, double Longitude) location,
            int limit = 1,
            CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<WeatherLocation[]>(
                 "geo/1.0/reverse?" +
                $"lat={location.Latitude.ToString(CultureInfo.InvariantCulture)}&" +
                $"lon={location.Longitude.ToString(CultureInfo.InvariantCulture)}&" +
                (limit <= 0 ? $"limit={int.MaxValue}&" : $"limit={limit}&") +
                $"appid={_ApiKey}",
                __JsonOptions,
                cancel)
                .ConfigureAwait(false);

        #endregion [Location]

        #region [Data]

        /// <summary> Get data by name </summary>
        public async Task<WeatherData> GetLocationData(
            string name,
            CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<WeatherData>(
                 "/data/2.5/forecast?" +
                $"q={name}&" +
                 "units=metric&" +
                $"appid={_ApiKey}",
                __JsonOptions,
                cancel)
                .ConfigureAwait(false);

        /// <summary> Get data by location (coordinates) </summary>
        public async Task<WeatherData> GetLocationData(
            (double Latitude, double Longitude) location,
            CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<WeatherData>(
                 "/data/2.5/forecast?" +
                $"lat={location.Latitude}&" +
                $"lon={location.Longitude}&" +
                 "units=metric&" +
                $"appid={_ApiKey}",
                __JsonOptions,
                cancel)
                .ConfigureAwait(false);

        /// <summary> Get data for <see cref="WeatherLocation"/> </summary>
        public async Task<WeatherData> GetLocationData(
            WeatherLocation location,
            CancellationToken cancel = default) => await GetLocationData(location.Name, cancel);

        #endregion [Data]
    }
}