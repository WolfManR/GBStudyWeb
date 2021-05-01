using MetricsManager.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    public class RamMetricsController : ApiController
    {
        [HttpGet("available/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableSpaceInfoFromAgent([FromRoute] RamMetricsFromAgentRequest request)
        {
            return Ok();
        }

        [HttpGet("available/cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableSpaceInfoFromAllCluster([FromRoute] RamMetricsFromAllClusterRequest request)
        {
            return Ok();
        }
    }
}