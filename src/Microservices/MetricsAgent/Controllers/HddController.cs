using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    public class HddController : ApiController
    {
        [HttpGet("left")]
        public IActionResult GetFreeHardDriveSpace()
        {
            return Ok();
        }
    }
}