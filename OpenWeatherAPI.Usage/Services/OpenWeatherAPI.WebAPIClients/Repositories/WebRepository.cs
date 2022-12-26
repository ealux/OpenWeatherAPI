using OpenWeatherAPI.Interfaces.Base.Entities;
using OpenWeatherAPI.Interfaces.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OpenWeatherAPI.WebAPIClients.Repositories
{
    public class WebRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly HttpClient _client;

        public WebRepository(HttpClient client) => this._client = client;

        #region [IRepository]

        public Task<T> Add(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Delete(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteById(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsById(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll(CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCount(CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        #endregion [IRepository]
    }
}