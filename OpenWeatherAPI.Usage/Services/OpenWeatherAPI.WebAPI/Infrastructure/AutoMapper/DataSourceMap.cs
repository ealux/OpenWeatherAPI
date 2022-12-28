using AutoMapper;

using OpenWeatherAPI.Domain.Base;
using OpenWeatherAPI.DAL.Entities;

namespace OpenWeatherAPI.WebAPI.Infrastructure.AutoMapper
{
    public class DataSourceMap : Profile
    {
        public DataSourceMap()
        {
            CreateMap<DataSourceInfo, DataSource>()
                .ReverseMap();
        }
    }
}
