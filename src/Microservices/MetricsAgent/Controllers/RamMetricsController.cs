using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    public class RamMetricsController : ApiController
    {
        [HttpGet("available")]
        public IActionResult GetAvailableSpaceInfo()
        {
            return Ok();
        }
    }
}