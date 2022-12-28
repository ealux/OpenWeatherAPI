using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenWeatherAPI.Interfaces.Base.Entities;
using OpenWeatherAPI.Interfaces.Base.Repositories;

namespace OpenWeatherAPI.WebAPI.Controllers.Base
{
    [ApiController, Route("api/[controller]")]
    public abstract class MappedEntityController<T, TBase> : ControllerBase
        where T : IEntity
        where TBase : IEntity
    {
        private readonly IRepository<TBase> _repository;
        private readonly IMapper _mapper;

        public MappedEntityController(IRepository<TBase> repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        #region [Get]

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetItemsCount() =>
            Ok(await _repository.GetCount());

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() =>
            Ok(GetItem(await _repository.GetAll()));

        [HttpGet("items[[{skip:int},{count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<T>>> Get(int skip, int count) =>
            Ok(GetItem(await _repository.Get(skip, count)));

        #region PageRecord

        protected record Page(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPage<T>
        {
            public int TotalPagesCount => (int)Math.Ceiling((double)TotalCount / PageSize);
        }
        protected IPage<T> GetPageItems(IPage<TBase> page) => new Page(GetItem(page.Items), page.TotalCount, page.PageIndex, page.PageSize);

        #endregion PageRecord

        [HttpGet("page/{pageIndex:int}/{pageSize:int}")]
        [HttpGet("page[[{pageIndex:int}/{pageSize:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IPage<T>>> GetPage(int pageIndex, int pageSize)
        {
            var result = await _repository.GetPage(pageIndex, pageSize);
            return result.Items.Any() 
                    ? Ok(GetPageItems(result)) 
                    : NotFound();
        }

        [HttpGet("{id:int}")]
        [ActionName("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id) =>
            await _repository.GetById(id) is { } item
                ? Ok(GetItem(item))
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
            await _repository.Exists(GetBase(item)) ? Ok(true) : NotFound(false);

        #endregion [Exists]

        #region [Add]

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(T item)
        {
            var result = await _repository.Add(GetBase(item));
            return CreatedAtAction(nameof(Get), new { Id = result.Id }, GetItem(result));
        }

        #endregion [Add]

        #region [Update]

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(T item)
        {
            if (await _repository.Update(GetBase(item)) is not { } result)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, GetItem(result));
        }

        #endregion [Update]

        #region [Delete]

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(T item)
        {
            if (await _repository.Delete(GetBase(item)) is not { } result)
                return NotFound(item);
            return Ok(GetItem(result));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(int id)
        {
            if (await _repository.DeleteById(id) is not { } result)
                return NotFound(id);
            return Ok(GetItem(result));
        }

        #endregion [Delete]

        #region [Utils]

        // single item mapping
        protected virtual TBase GetBase(T item) => _mapper.Map<TBase>(item);

        protected virtual T GetItem(TBase baseItem) => _mapper.Map<T>(baseItem);

        // enumerable items mapping
        protected virtual IEnumerable<TBase> GetBase(IEnumerable<T> item) => _mapper.Map<IEnumerable<TBase>>(item);

        protected virtual IEnumerable<T> GetItem(IEnumerable<TBase> baseItem) => _mapper.Map<IEnumerable<T>>(baseItem);

        #endregion [Utils]
    }
}