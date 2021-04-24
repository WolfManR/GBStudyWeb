using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    public class DotnetMetricsController : ApiController
    {
        [HttpGet("errors-count/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountFromAgent(
            [FromRoute] int agentId, 
            [FromRoute] TimeSpan fromTime, 
            [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

        [HttpGet("errors-count/cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountFromAllCluster(
            [FromRoute] TimeSpan fromTime, 
            [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}