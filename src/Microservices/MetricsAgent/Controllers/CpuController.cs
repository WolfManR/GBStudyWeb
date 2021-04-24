using MetricsAgent.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    public class CpuController : ApiController
    {
        [HttpGet("from/{fromTime:datetime}/to/{toTime:datetime}/percentiles/{percentile}")]
        public IActionResult GetMetrics([FromRoute] GetCpuMetricsByPercentilesRequest request)
        {
            return Ok();
        }

        [HttpGet("from/{fromTime:datetime}/to/{toTime:datetime}")]
        public IActionResult GetMetrics([FromRoute] GetCpuMetricsRequest request)
        {
            return Ok();
        }
    }
}