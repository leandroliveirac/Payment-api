using Microsoft.AspNetCore.Mvc;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;

namespace Payment_api.WebAPI.Controllers
{
    [ApiController]
    [Route("api-docs/[controller]")]
    [Produces("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetAllAsync()
        {
            var categories = await _categoryAppService.GetAllAsync();

            if (categories == null)
                return NotFound();

            return Ok(categories);
        }

        [HttpGet]
        [Route("{description}",Name = "getByDescription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryViewModel>> GetByDescriptionAsync([FromRoute] string description)
        {
            var category = await _categoryAppService.GetByDescriptionAsync(description);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryInputModel entity)
        {
            try
            {
                var category = await _categoryAppService.CreateAsync(entity);
                return CreatedAtRoute("getByDescription", new {description = category.Description },category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] CategoryInputModel entity)
        {
            try
            {
                var category = _categoryAppService.Update(id, entity);
                return CreatedAtRoute("getByDescription", new { description = entity.Description }, category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
