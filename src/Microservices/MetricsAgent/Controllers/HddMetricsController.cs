using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    public class HddMetricsController : ApiController
    {
        [HttpGet("left")]
        public IActionResult GetFreeHardDriveSpace()
        {
            return Ok();
        }
    }
}