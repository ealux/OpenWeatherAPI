using OpenWeatherAPI.Interfaces.Base.Entities;
using OpenWeatherAPI.Interfaces.Base.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OpenWeatherAPI.WebAPIClients.Repositories
{
    public class WebRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly HttpClient _client;

        public WebRepository(HttpClient client) => this._client = client;

        #region [IRepository]

        #region [Get]

        public async Task<int> GetCount(CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<int>("count", cancel).ConfigureAwait(false);

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<IEnumerable<T>>("", cancel).ConfigureAwait(false);

        public async Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<IEnumerable<T>>($"items[{skip},{count}]", cancel).ConfigureAwait(false);

        public async Task<T> GetById(int id, CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<T>($"{id}", cancel).ConfigureAwait(false);

        public async Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            var response = await _client.GetAsync($"page[{pageIndex}/{pageSize}]", cancel).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new PageItems
                {
                    Items = Enumerable.Empty<T>(),
                    PageIndex = pageIndex,
                    PageSize = pageIndex,
                    TotalCount = 0
                };
            }
            return await response
                    .EnsureSuccessStatusCode()
                    .Content
                    .ReadFromJsonAsync<PageItems>(cancellationToken: cancel)
                    .ConfigureAwait(false);
        }

        #region [Helper Page model]

        private class PageItems : IPage<T>
        {
            public IEnumerable<T> Items { get; init; }

            public int TotalCount { get; init; }

            public int PageIndex { get; init; }

            public int PageSize { get; init; }
        }

        #endregion [Helper Page model]

        #endregion [Get]

        #region [Exists]

        public async Task<bool> Exists(T item, CancellationToken cancel = default)
        {
            var response = await _client.PostAsJsonAsync("exists", cancel).ConfigureAwait(false);
            return response.StatusCode != HttpStatusCode.NotFound && response.IsSuccessStatusCode;
        }

        public async Task<bool> ExistsById(int id, CancellationToken cancel = default)
        {
            var response = await _client.GetAsync($"exists/id/{id}", cancel).ConfigureAwait(false);
            return response.StatusCode != HttpStatusCode.NotFound && response.IsSuccessStatusCode;
        }

        #endregion [Exists]

        #region [Add]

        public async Task<T> Add(T item, CancellationToken cancel = default)
        {
            var response = await _client.PostAsJsonAsync("", item, cancel).ConfigureAwait(false);
            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);
            return result;
        }

        #endregion [Add]

        #region [Update]

        public async Task<T> Update(T item, CancellationToken cancel = default)
        {
            var response = await _client.PutAsJsonAsync("", item, cancel).ConfigureAwait(false);
            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);
            return result;
        }

        #endregion [Update]

        #region [Delete]

        public async Task<T> Delete(T item, CancellationToken cancel = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "")
            {
                Content = JsonContent.Create(item)
            };
            var response = await _client.SendAsync(request, cancel).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return default;

            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);
            return result;
        }

        public async Task<T> DeleteById(int id, CancellationToken cancel = default)
        {
            var response = await _client.DeleteAsync($"{id}", cancel).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return default;

            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);
            return result;
        }

        #endregion [Delete]

        #endregion [IRepository]
    }
}