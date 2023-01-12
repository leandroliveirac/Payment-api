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
        private readonly IOrderAppService _orderService;

        public OrderController(IOrderAppService orderService)
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
        [Route("{id:Guid}", Name = "getOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderViewModel>> GetByIdAsync([FromRoute] Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);

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
                var order = await _orderService.CreateAsync(entity);
                return CreatedAtRoute("getOrderById", new {id = order.Id}, order);
            }
            catch (Exception ex)
            {                
                return BadRequest($"An error occurred while trying to execute your request: \n {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpPut("canceled/{id:Guid}")]
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