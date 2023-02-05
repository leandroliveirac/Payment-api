using Microsoft.AspNetCore.Mvc;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;

namespace Payment_api.WebAPI.Controllers
{
    [ApiController]
    [Route("api-docs/[controller]")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productAppService.GetAllAsync();

            if(products == null || products.Count() <= 0)
                return NotFound();
            
            return Ok(products);
        }

        [HttpGet]
        [Route("id/{id}", Name ="getProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)        
        {
            var product = await _productAppService.GetByIdAsync(id);

            if(product == null)
                return NotFound();
            
            return Ok(product);
        }

        [HttpGet]
        [Route("description/{description}", Name ="getByDescriptionProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByDescriptionAsync([FromRoute] string description)
        {
            var products = await _productAppService.GetByDescriptionAsync(description);

            if(products == null || products.Count()<= 0)
                return NotFound();
            
            return Ok(products);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(ProductInputModel entity)
        {
            try
            {
                var product = await _productAppService.CreateAsync(entity);
                return CreatedAtRoute("getProductById", new { id = product.Id }, product);
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
        

        [HttpPost("inactivate/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Inactivate([FromRoute] Guid id)
        {
            try
            {
                _productAppService.Inactivate(id);
                return Ok();
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