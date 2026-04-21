using Microsoft.AspNetCore.Mvc;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;

namespace Payment_api.WebAPI.Controllers
{
    public class CategoryController : MainController
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }


        /// <summary>
        /// Lista categorias
        /// </summary>
        /// <response code="200">Retorna uma lista de categoria.</response>
        /// <response code="404">Não encontrado.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryViewModel>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categoryAppService.GetAllAsync();

            if (categories == null)
                return NotFound();

            return Ok(categories);
        }

        /// <summary>
        /// Busca por descrição da categoria
        /// </summary>
        /// <param name="description">Descrição da categoria</param>
        /// <response code="200"> Retorna categoria encontrada.</response>
        /// <response code="404">Não encontrado.</response>
        [HttpGet]
        [Route("{description}",Name = "getByDescription")]
        [ProducesResponseType(typeof(CategoryViewModel),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByDescriptionAsync([FromRoute] string description)
        {
            var category = await _categoryAppService.GetByDescriptionAsync(description);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        /// <summary>
        /// Adiciona categoria
        /// </summary>
        /// <param name="request">Categoria</param>
        /// <response code="201"> Retorna categoria encontrada.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CategoryViewModel),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryInputModel request)
        {
            try
            {
                var category = await _categoryAppService.CreateAsync(request);
                return CreatedAtRoute("getByDescription", new {description = category.Description },category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.InnerException?.Message ?? ex.Message });
            }
        }


        /// <summary>
        /// Atualiza categoria
        /// </summary>
        /// <param name="request">Categoria</param>
        /// <param name="id">codigo da categoria</param>
        /// <response code="201"> Retorna categoria encontrada.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(CategoryViewModel),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CategoryInputModel request)
        {
            try
            {
                var category = await _categoryAppService.UpdateAsync(id, request);
                return CreatedAtRoute("getByDescription", new { description = request.Description }, category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
