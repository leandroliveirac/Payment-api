using Microsoft.AspNetCore.Mvc;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;

namespace Payment_api.WebAPI.Controllers
{
    public class SellerController : MainController
    {
        private readonly ISellerAppService _sellerService;

        public SellerController(ISellerAppService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SellerViewModel>),StatusCodes.Status200OK)]
        public async Task<IActionResult>GetAll()
        {
            var seller = await _sellerService.GetAllAsync();           
            return Ok(seller);
        }

        [HttpGet]
        [Route("{id:Guid}", Name = "getSellerById")]
        [ProducesResponseType(typeof(SellerViewModel),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var seller = await _sellerService.GetByIdAsync(id);

            if(seller == null)
                return NotFound();

            return Ok(seller);
        }

        [HttpPost]        
        [ProducesResponseType(typeof(SellerViewModel),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(SellerViewModel),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] SellerInputModel entity)
        {
            try
            {
                var seller = await _sellerService.UpdateAsync(entity, id);
                return CreatedAtRoute("getSellerById", new { id = seller.Id }, seller);
            }
            catch (Exception ex)
            {                
                return BadRequest($"An error occurred while trying to execute your request: \n {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }
}