using OpenWeatherAPI.Interfaces.Base.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OpenWeatherAPI.Interfaces.Base.Repositories
{
    public interface INamedRepository<T> : IRepository<T> where T : INamedEntity
    {
        Task<bool> ExistsName(string name, CancellationToken cancel = default);

        Task<T> GetByName(string name, CancellationToken cancel = default);

        Task<T> DeleteByName(string name, CancellationToken cancel = default);
    }
}