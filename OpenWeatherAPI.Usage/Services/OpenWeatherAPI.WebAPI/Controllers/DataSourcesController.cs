using OpenWeatherAPI.DAL.Entities;
using OpenWeatherAPI.Interfaces.Base.Repositories;
using OpenWeatherAPI.WebAPI.Controllers.Base;

namespace OpenWeatherAPI.WebAPI.Controllers
{
    public class DataSourcesController : EntityController<DataSource>
    {
        public DataSourcesController(IRepository<DataSource> repository) : base(repository)
        {
        }
    }
}