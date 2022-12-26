using OpenWeatherAPI.DAL.Entities;
using OpenWeatherAPI.Interfaces.Base.Repositories;
using OpenWeatherAPI.WebAPI.Controllers.Base;

namespace OpenWeatherAPI.WebAPI.Controllers
{
    public class DataValueController : EntityController<DataValue>
    {
        public DataValueController(IRepository<DataValue> repository) : base(repository)
        {
        }
    }
}