using Microsoft.AspNetCore.Mvc;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;

namespace Payment_api.WebAPI.Controllers
{
    [ApiController]
    [Route("api-docs/[controller]")]
    [Produces("application/json")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleAppService _saleService;

        public SaleController(ISaleAppService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        [Route("{id:Guid}", Name = "getSaleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SaleViewModel>> GetByIdAsync([FromRoute] Guid id)
        {
            var sale = await _saleService.GetByIdAsync(id);

            if(sale == null)
                return NotFound();

            return Ok(sale);
        }

        [HttpPost]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SaleViewModel>> CreateAsync([FromBody] SaleInputModel entity)
        {         

            try
            {
                var sale = await _saleService.CreateAsync(entity);
                return CreatedAtRoute("getSaleById",new {id = sale.Id},sale);
            }
            catch (Exception ex)
            {                
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("canceled/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Canceled([FromRoute] Guid id)
        {
            try
            {
                _saleService.Canceled(id);
                return NoContent();
            }
            catch (Exception ex)
            {                
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("payment-accept/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PaymentAccept([FromRoute] Guid id)
        {
            try
            {
                _saleService.PaymentAccept(id);
                return NoContent();
            }
            catch (Exception ex)
            {                
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("sent-carrier/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SentCarrier([FromRoute] Guid id)
        {
            try
            {
                _saleService.SentCarrier(id);
                return NoContent();
            }
            catch (Exception ex)
            {                
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("delivered/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delivered([FromRoute] Guid id)
        {
            try
            {
                _saleService.Delivered(id);
                return NoContent();
            }
            catch (Exception ex)
            {                
                return BadRequest($"{ex.Message}");
            }
        }

    }
}