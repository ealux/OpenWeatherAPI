using Microsoft.EntityFrameworkCore;
using OpenWeatherAPI.DAL.Context;
using OpenWeatherAPI.DAL.Entities.Base;
using OpenWeatherAPI.Interfaces.Base.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OpenWeatherAPI.DAL.Repositories
{
    public class DBNamedRepository<T> : DBRepository<T>, INamedRepository<T> where T : NamedEntity, new()
    {
        public DBNamedRepository(DataDB db) : base(db)
        {
        }

        public async Task<T> DeleteByName(string name, CancellationToken cancel = default)
        {
            var item = Set.Local.FirstOrDefault(x => x.Name == name);

            item ??= await Set.Select(x => new T { Id = x.Id, Name = x.Name })
                                .FirstOrDefaultAsync(x => x.Name == name, cancel)
                                .ConfigureAwait(false);

            return item is null ? null : await Delete(item, cancel).ConfigureAwait(false);
        }

        public async Task<bool> ExistsName(string name, CancellationToken cancel = default) =>
            await Items.AnyAsync(x => x.Name == name, cancel).ConfigureAwait(false);

        public async Task<T> GetByName(string name, CancellationToken cancel = default) =>
            await Items.FirstOrDefaultAsync(x => x.Name == name, cancel).ConfigureAwait(false);
    }
}