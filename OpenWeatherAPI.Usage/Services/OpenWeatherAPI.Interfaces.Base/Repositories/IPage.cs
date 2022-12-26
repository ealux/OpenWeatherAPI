using System;
using System.Collections.Generic;

namespace OpenWeatherAPI.Interfaces.Base.Repositories
{
    public interface IPage<out T>
    {
        IEnumerable<T> Items { get; }

        int TotalCount { get; }
        int PageIndex { get; }
        int PageSize { get; }

        int TotalPagesCount => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}