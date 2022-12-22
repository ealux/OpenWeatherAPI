using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using OpenWeatherAPI.Models;
using OpenWeatherAPI.Converters;

namespace OpenWeatherAPI
{
    public class WeatherClient
    {
        private const string _ApiKey = "c082f7450a91dda8d92fff5710d75da5";
        private readonly HttpClient _client;

        private static readonly JsonSerializerOptions __JsonOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter(),
                new JsonInt32ToDateTimeConverter(),
                new JsonInt64ToDateTimeConverter(),
                new JsonStringToDateTimeConverter()
            }
        };

        public WeatherClient(HttpClient client) => this._client = client;

        #region [Location]

        /// <summary> Get Location info (massive) by city name </summary>
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

        /// <summary> Get Location info (massive) by city coordinates </summary>
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

        #region [Data Forecast]

        /// <summary> Get forecast weather data (on 5 days) by name </summary>
        public async Task<WeatherForecastData> GetForecastData(
            string name,
            CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<WeatherForecastData>(
                 "/data/2.5/forecast?" +
                $"q={name}&" +
                 "units=metric&" +
                $"appid={_ApiKey}",
                __JsonOptions,
                cancel)
                .ConfigureAwait(false);

        /// <summary> Get forecast weather data (on 5 days) by location (coordinates) </summary>
        public async Task<WeatherForecastData> GetForecastData(
            (double Latitude, double Longitude) location,
            CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<WeatherForecastData>(
                 "/data/2.5/forecast?" +
                $"lat={location.Latitude}&" +
                $"lon={location.Longitude}&" +
                 "units=metric&" +
                $"appid={_ApiKey}",
                __JsonOptions,
                cancel)
                .ConfigureAwait(false);

        /// <summary> Get forecast weather data (on 5 days) for <see cref="WeatherLocation"/> </summary>
        public Task<WeatherForecastData> GetForecastData(WeatherLocation location, CancellationToken cancel = default) =>
            GetForecastData(location.Name, cancel);

        #endregion [Data Forecast]

        #region [Data Current]

        /// <summary> Get current weather data by name </summary>
        public async Task<WeatherCurrentData> GetCurrentData(
            string name,
            CancellationToken cancel = default)
        {
            var raw_data = await _client.GetFromJsonAsync<WeatherCurrentDataInternal>(
                 "/data/2.5/weather?" +
                $"q={name}&" +
                 "units=metric&" +
                $"appid={_ApiKey}",
                __JsonOptions,
                cancel)
                .ConfigureAwait(false);

            return await Task.FromResult(new WeatherCurrentData
            {
                City = new City
                {
                    Country = raw_data.City.Country,
                    Id = raw_data.City.Id,
                    Location = raw_data.Location,
                    Name = raw_data.Name,
                    Population = raw_data.City.Population,
                    SunRiseUTC = raw_data.City.SunRiseUTC,
                    SunSetUTC = raw_data.City.SunSetUTC,
                    TimezoneFromUTC = raw_data.City.TimezoneFromUTC
                },
                Clouds = raw_data.Clouds,
                TimestampUTC = raw_data.TimestampUTC,
                Visibility = raw_data.Visibility,
                Weather = raw_data.Weather,
                Wind = raw_data.Wind
            })
            .ConfigureAwait(false);
        }

        /// <summary> Get current weather data by location (coordinates) </summary>
        public async Task<WeatherCurrentData> GetCurrentData(
            (double Latitude, double Longitude) location,
            CancellationToken cancel = default)
        {
            var raw_data = await _client.GetFromJsonAsync<WeatherCurrentDataInternal>(
                 "/data/2.5/weather?" +
                $"lat={location.Latitude}&" +
                $"lon={location.Longitude}&" +
                 "units=metric&" +
                $"appid={_ApiKey}",
                __JsonOptions,
                cancel)
                .ConfigureAwait(false);

            return await Task.FromResult(new WeatherCurrentData
            {
                City = new City
                {
                    Country = raw_data.City.Country,
                    Id = raw_data.City.Id,
                    Location = raw_data.Location,
                    Name = raw_data.Name,
                    Population = raw_data.City.Population,
                    SunRiseUTC = raw_data.City.SunRiseUTC,
                    SunSetUTC = raw_data.City.SunSetUTC,
                    TimezoneFromUTC = raw_data.City.TimezoneFromUTC
                },
                Clouds = raw_data.Clouds,
                TimestampUTC = raw_data.TimestampUTC,
                Visibility = raw_data.Visibility,
                Weather = raw_data.Weather,
                Wind = raw_data.Wind
            })
            .ConfigureAwait(false);
        }
            

        /// <summary> Get current weather data for <see cref="WeatherLocation"/> </summary>
        public Task<WeatherCurrentData> GetCurrentData(WeatherLocation location, CancellationToken cancel = default) =>
            GetCurrentData(location.Name, cancel);

        #endregion [Weather]
    }
}