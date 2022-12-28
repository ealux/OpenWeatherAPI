using Microsoft.AspNetCore.Mvc;

namespace OpenWeatherAPI.WebAPI.Controllers.Base
{
    [ApiController, Route("api/[controller]")]
    public abstract class MappedEntityController<T, TBase> : ControllerBase
    {
        
    }
}
