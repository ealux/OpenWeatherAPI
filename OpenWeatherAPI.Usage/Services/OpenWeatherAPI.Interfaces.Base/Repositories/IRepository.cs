using OpenWeatherAPI.Interfaces.Base.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OpenWeatherAPI.Interfaces.Base.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        #region [Base]

        Task<IEnumerable<T>> GetAll(CancellationToken cancel = default);

        Task<T> GetById(int id, CancellationToken cancel = default);

        Task<T> Add(T item, CancellationToken cancel = default);

        Task<T> Update(T item, CancellationToken cancel = default);

        Task<T> Delete(T item, CancellationToken cancel = default);

        #endregion [Base]

        #region [Extensions]

        Task<int> GetCount(CancellationToken cancel = default);

        Task<bool> ExistsById(int id, CancellationToken cancel = default);

        Task<bool> Exists(T item, CancellationToken cancel = default);

        Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancel = default);

        Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default);

        Task<T> DeleteById(int id, CancellationToken cancel = default);

        #endregion [Extensions]
    }
}