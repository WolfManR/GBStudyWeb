using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    public class RamController : ApiController
    {
        [HttpGet("available")]
        public IActionResult GetAvailableSpaceInfo()
        {
            return Ok();
        }
    }
}