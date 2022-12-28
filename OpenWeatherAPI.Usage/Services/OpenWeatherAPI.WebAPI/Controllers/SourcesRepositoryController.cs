using AutoMapper;
using OpenWeatherAPI.DAL.Entities;
using OpenWeatherAPI.Domain.Base;
using OpenWeatherAPI.Interfaces.Base.Repositories;
using OpenWeatherAPI.WebAPI.Controllers.Base;

namespace OpenWeatherAPI.WebAPI.Controllers
{
    public class SourcesRepositoryController : MappedEntityController<DataSourceInfo, DataSource>
    {
        public SourcesRepositoryController(IRepository<DataSource> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}