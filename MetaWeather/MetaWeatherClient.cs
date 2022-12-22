using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Threading;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MetaWeather
{
    public class MetaWeatherClient
    {
        private readonly HttpClient _client;
        private static readonly JsonSerializerOptions __JsonOptions = new JsonSerializerOptions 
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

        public MetaWeatherClient(HttpClient client) => this._client = client;

        // Get data for Location
        public async Task<WeatherLocation[]> GetLocationByName(string name, CancellationToken cancel = default)
        {
            return await _client.GetFromJsonAsync<WeatherLocation[]>(
                $"/geo/1.0/direct?q={name}&limit=1&appid=c082f7450a91dda8d92fff5710d75da5",
                __JsonOptions,
                cancel)
                .ConfigureAwait(false);
        }
        
    }
}
