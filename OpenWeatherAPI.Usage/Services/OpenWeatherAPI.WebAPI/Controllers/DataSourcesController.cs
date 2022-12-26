using Microsoft.AspNetCore.Mvc;
using OpenWeatherAPI.DAL.Entities;
using OpenWeatherAPI.Interfaces.Base.Repositories;

namespace OpenWeatherAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSourcesController : ControllerBase
    {
        private readonly IRepository<DataSource> _repository;

        public DataSourcesController(IRepository<DataSource> repository) => this._repository = repository;

        #region [Get]

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetItemsCount() => Ok(await _repository.GetCount());

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAll());

        [HttpGet("items[[{skip:int}:{count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DataSource>>> Get(int skip, int count) => Ok(await _repository.Get(skip, count));

        [HttpGet("page/{pageIndex:int}/{pageSize:int}")]
        [HttpGet("page[[{pageIndex:int}:{pageSize:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IPage<DataSource>>> GetPage(int pageIndex, int pageSize)
        {
            var result = await _repository.GetPage(pageIndex, pageSize);
            return result.Items.Any() ? Ok(result) : NotFound(result);
        }

        [HttpGet("{id:int}")]
        [ActionName("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id) =>
            await _repository.GetById(id) is { } item
                ? Ok(item)
                : NotFound();

        #endregion [Get]

        #region [Exists]

        [HttpGet("exists/id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> ExistsId(int id) => await _repository.ExistsById(id) ? Ok(true) : NotFound(false);

        [HttpGet("exists")]
        [HttpPost("exists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Exists(DataSource item) => await _repository.Exists(item) ? Ok(true) : NotFound(false);

        #endregion [Exists]

        #region [Add]

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(DataSource item)
        {
            var result = await _repository.Add(item);
            return CreatedAtAction(nameof(Get), new { Id = result.Id });
        }

        #endregion [Add]

        #region [Update]

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(DataSource item)
        {
            if (await _repository.Update(item) is not { } result)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.Id });
        }

        #endregion [Update]

        #region [Delete]

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(DataSource item)
        {
            if (await _repository.Delete(item) is not { } result)
                return NotFound(item);
            return Ok(item);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(int id)
        {
            if (await _repository.DeleteById(id) is not { } result)
                return NotFound(id);
            return Ok(id);
        }

        #endregion [Delete]
    }
}