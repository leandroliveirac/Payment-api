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
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetAllAsync()
        {
            var categories = await _categoryService.GetAllAsync();

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
            var category = await _categoryService.GetByDescriptionAsync(description);

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
                var category = await _categoryService.CreateAsync(entity);
                return CreatedAtRoute("getByDescription", new {description = category.Description },category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromQuery] Guid id, [FromQuery] string newDescription)
        {
            try
            {
                var category = _categoryService.Update(id, newDescription);
                return CreatedAtRoute("getByDescription", new { description = newDescription }, category);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new { Error = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Remove([FromBody] CategoryInputModel entity)
        {
            try
            {
                _categoryService.Remove(entity);
                return NoContent();
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new { Error = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
