using Microsoft.AspNetCore.Mvc;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;

namespace Payment_api.WebAPI.Controllers
{
    [ApiController]
    [Route("api-docs/[controller]")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetAll()
        {
            var order = await _orderService.GetAllAsync();

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet]
        [Route("{orderId}", Name = "getOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderViewModel>> GetByIdAsync([FromRoute] Guid orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);

            if(order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] OrderInputModel entity)
        {
            try
            {
                await _orderService.CreateAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {                
                return BadRequest($"An error occurred while trying to execute your request: \n {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpPut("canceled")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Canceled(Guid id)
        {
            try
            {
                _orderService.Canceled(id);
                return NoContent();
            }
            catch (Exception ex)
            {                
                return BadRequest($"An error occurred while trying to execute your request: \n {ex.Message}");
            }
        }        
    }
}