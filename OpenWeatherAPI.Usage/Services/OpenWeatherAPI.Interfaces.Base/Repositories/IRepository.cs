using OpenWeatherAPI.Interfaces.Base.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenWeatherAPI.Interfaces.Base.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        #region [Base]

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task<T> Add(T item);

        Task<T> Update(T item);

        Task<T> Delete(T item);

        #endregion [Base]

        #region [Extensions]

        Task<int> GetCount();

        Task<bool> ExistsById(int id);

        Task<bool> Exists(T item);

        Task<IEnumerable<T>> Get(int skip, int count);

        Task<IPage<T>> GetPage(int pageIndex, int pageSize);

        Task<T> DeleteById(int id);

        #endregion [Extensions]
    }
}