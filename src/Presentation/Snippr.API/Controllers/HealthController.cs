using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Snippr.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        public IActionResult HealthCheck() => Ok("200 A-OK!");
    }
}
