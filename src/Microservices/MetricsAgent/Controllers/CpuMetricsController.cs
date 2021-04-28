using MetricsAgent.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    public class CpuMetricsController : ApiController
    {
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] CpuMetricsRequest request)
        {
            return Ok();
        }
    }
}