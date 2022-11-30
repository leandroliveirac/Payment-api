using Microsoft.AspNetCore.Mvc;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;

namespace Payment_api.WebAPI.Controllers
{
    [ApiController]
    [Route("api-docs/[controller]")]
    [Produces("application/json")]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet]
        [Route("{id}", Name = "getSellerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SellerViewModel>> GetByIdAsync([FromRoute] Guid id)
        {
            var seller = await _sellerService.GetByIdAsync(id);

            if(seller == null)
                return NotFound();

            return Ok(seller);
        }

        [HttpPost]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] SellerInputModel entity)
        {
            try
            {
                var seller = await _sellerService.CreateAsync(entity);
                return CreatedAtRoute("getSellerById", new { id = seller.Id },seller);
            }
            catch (Exception ex)
            {                
                return BadRequest($"An error occurred while trying to execute your request: \n {ex.InnerException?.Message ?? ex.Message}");
            }
        }


        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SellerViewModel> Update([FromRoute] Guid id, [FromBody] SellerInputModel entity)
        {
            try
            {
                var seller = _sellerService.Update(entity, id);
                return CreatedAtRoute("getSellerById", new { id = seller.Id }, seller);
            }
            catch (Exception ex)
            {                
                return BadRequest($"An error occurred while trying to execute your request: \n {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                _sellerService.Remove(id);
                return NoContent();
            }
            catch (Exception ex)
            {                
                return BadRequest($"An error occurred while trying to execute your request: \n {ex.InnerException?.Message ?? ex.Message}");
            }
        }

    }
}