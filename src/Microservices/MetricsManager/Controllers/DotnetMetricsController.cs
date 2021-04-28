using System;
using MetricsManager.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    public class DotnetMetricsController : ApiController
    {
        [HttpGet("errors-count/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountFromAgent(
            [FromRoute] GetErrorsCountFromAgentRequest request)
        {
            return Ok();
        }

        [HttpGet("errors-count/cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountFromAllCluster(
            [FromRoute] GetErrorsCountFromAllClusterRequest request)
        {
            return Ok();
        }
    }
}