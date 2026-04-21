using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace Payment_api.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class MainController : ControllerBase
    {
    }
}
