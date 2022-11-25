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
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productService.GetAllAsync();

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
            var product = await _productService.GetByIdAsync(id);

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
            var products = await _productService.GetByDescriptionAsync(description);

            if(products == null || products.Count()<= 0)
                return NotFound();
            
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(ProductInputModel entity)
        {
            try
            {
                var product = await _productService.CreateAsync(entity);
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody] ProductInputModel entity, Guid productId)
        {
            try
            {
                var product = _productService.Update(entity,productId);
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Remove([FromBody] ProductInputModel entity, Guid productId)
        {
            try
            {
                _productService.Remove(entity,productId);
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