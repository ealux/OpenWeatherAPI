using Microsoft.AspNetCore.Mvc;
using OpenWeatherAPI.DAL.Entities;
using OpenWeatherAPI.DAL.Entities.Base;
using OpenWeatherAPI.Interfaces.Base.Repositories;

namespace OpenWeatherAPI.WebAPI.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class EntityController<T> : ControllerBase where T : Entity
    {
        private readonly IRepository<T> _repository;

        protected EntityController(IRepository<T> _repository) => this._repository = _repository;

        #region [Get]

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetItemsCount() =>
            Ok(await _repository.GetCount());

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repository.GetAll());

        [HttpGet("items[[{skip:int},{count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<T>>> Get(int skip, int count) =>
            Ok(await _repository.Get(skip, count));

        [HttpGet("page/{pageIndex:int}/{pageSize:int}")]
        [HttpGet("page[[{pageIndex:int}/{pageSize:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IPage<T>>> GetPage(int pageIndex, int pageSize)
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
        public async Task<IActionResult> ExistsId(int id) =>
            await _repository.ExistsById(id) ? Ok(true) : NotFound(false);

        [HttpPost("exists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Exists(T item) =>
            await _repository.Exists(item) ? Ok(true) : NotFound(false);

        #endregion [Exists]

        #region [Add]

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(T item)
        {
            var result = await _repository.Add(item);
            return CreatedAtAction(nameof(Get), new { Id = result.Id }, result);
        }

        #endregion [Add]

        #region [Update]

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(T item)
        {
            if (await _repository.Update(item) is not { } result)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        #endregion [Update]

        #region [Delete]

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(T item)
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