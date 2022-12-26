using Microsoft.EntityFrameworkCore;
using OpenWeatherAPI.DAL.Context;
using OpenWeatherAPI.DAL.Entities.Base;
using OpenWeatherAPI.Interfaces.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OpenWeatherAPI.DAL.Repositories
{
    public class DBRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DataDB _db;

        protected DbSet<T> Set { get; }

        protected virtual IQueryable<T> Items => Set;

        public bool AutoSaveChanges { get; set; } = true;

        // ctor
        public DBRepository(DataDB db)
        {
            this._db = db;
            Set = _db.Set<T>();
        }

        #region [IRepository methods]

        public async Task<T> Add(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            await _db.AddAsync(item, cancel).ConfigureAwait(false);
            if (AutoSaveChanges)
                await SaveChanges(cancel).ConfigureAwait(false);

            return item;
        }

        public async Task<T> Delete(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            if (!await ExistsById(item.Id, cancel))
                return null;

            _db.Remove(item);

            if (AutoSaveChanges)
                await SaveChanges(cancel).ConfigureAwait(false);

            return item;
        }

        public async Task<T> DeleteById(int id, CancellationToken cancel = default)
        {
            var item = Set.Local.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item = await Set.Select(_ => new T { Id = item.Id })
                                .FirstOrDefaultAsync(x => x.Id == id, cancel)
                                .ConfigureAwait(false);
            }
            if (item is null)
                return null;

            return await Delete(item, cancel).ConfigureAwait(false);
        }

        public async Task<bool> Exists(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            return await Items.AnyAsync(x => x.Id == item.Id, cancel).ConfigureAwait(false);
        }

        public async Task<bool> ExistsById(int id, CancellationToken cancel = default) =>
            await Items.AnyAsync(x => x.Id == id, cancel).ConfigureAwait(false);

        public async Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancel = default)
        {
            if (count <= 0)
                return Enumerable.Empty<T>();

            IQueryable<T> query = Items switch
            {
                IOrderedQueryable<T> ordered_query => ordered_query,
                { } q => q.OrderBy(x => x.Id)
            };

            if (skip > 0)
                query = query.Skip(skip);
            return await query.Take(count).ToArrayAsync(cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancel = default) =>
            await Items.ToArrayAsync(cancel).ConfigureAwait(false);

        public async Task<T> GetById(int id, CancellationToken cancel = default)
        {
            return Items switch
            {
                DbSet<T> set => await set.FindAsync(new object[] { id }, cancel).ConfigureAwait(false),
                { } items => await items.FirstOrDefaultAsync(x => x.Id == id, cancel).ConfigureAwait(false),
                _ => throw new InvalidOperationException("Error at data source type identification"),
            };
        }

        public async Task<int> GetCount(CancellationToken cancel = default) =>
            await Items.CountAsync(cancel).ConfigureAwait(false);

        public async Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            if (pageSize <= 0) return new Page(Enumerable.Empty<T>(), pageSize, pageIndex, pageSize);

            var query = Items;
            var total_count = await query.CountAsync(cancel).ConfigureAwait(false);
            if (total_count == 0)
                return new Page(Enumerable.Empty<T>(), 0, pageIndex, pageSize);

            if (query is not IOrderedQueryable<T>)
                query = query.OrderBy(x => x.Id);

            if (pageIndex > 0)
                query = query.Skip(pageIndex * pageSize);
            query = query.Take(pageSize);

            var items = await query.ToArrayAsync(cancel).ConfigureAwait(false);

            return new Page(items, total_count, pageIndex, pageSize);
        }

        public async Task<T> Update(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Update(item);

            if (AutoSaveChanges)
                await SaveChanges(cancel).ConfigureAwait(false);

            return item;
        }

        #endregion [IRepository methods]

        #region [Utils]

        private async Task SaveChanges(CancellationToken cancel) =>
            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);

        protected record Page(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPage<T>
        {
            public int TotalPagesCount => (int)Math.Ceiling((double)TotalCount / PageSize);
        }

        #endregion [Utils]
    }
}