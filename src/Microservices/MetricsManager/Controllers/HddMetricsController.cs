using MetricsManager.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    public class HddMetricsController : ApiController
    {
        [HttpGet("left/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetLeftSpaceOnHddFromAgent([FromRoute] HddMetricsFromAgentRequest request)
        {
            return Ok();
        }

        [HttpGet("left/cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetLeftSpaceOnHddFromAllCluster([FromRoute] HddMetricsFromAllClusterRequest request)
        {
            return Ok();
        }
    }
}