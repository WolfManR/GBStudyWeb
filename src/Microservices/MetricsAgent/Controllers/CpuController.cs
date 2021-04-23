using System;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    public class CpuController : ApiController
    {
        [HttpGet("from/{fromTime:datetime}/to/{toTime:datetime}/percentiles/{percentile}")]
        public IActionResult GetMetrics(
            [FromRoute] DateTime fromTime, 
            [FromRoute] DateTime toTime,
            [FromRoute] Percentiles percentile)
        {
            return Ok();
        }

        [HttpGet("from/{fromTime:datetime}/to/{toTime:datetime}")]
        public IActionResult GetMetrics([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            return Ok();
        }
    }
}