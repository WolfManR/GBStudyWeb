using MetricsAgent.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    public class RamMetricsController : ApiController
    {
        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableSpaceInfo([FromRoute] AvailableSpaceInfoRequest request)
        {
            return Ok();
        }
    }
}