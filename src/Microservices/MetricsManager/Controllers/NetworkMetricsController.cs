using System;
using MetricsManager.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    public class NetworkMetricsController : ApiController
    {
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] GetNetworkMetricsFromAgentRequest request)
        {
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster(
            [FromRoute] GetNetworkMetricsFromAllClusterRequest request)
        {
            return Ok();
        }
    }
}