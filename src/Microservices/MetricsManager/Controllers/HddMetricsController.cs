using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    public class HddMetricsController : ApiController
    {
        [HttpGet("left/agent/{agentId}")]
        public IActionResult GetLeftSpaceOnHddFromAgent([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpGet("left/cluster")]
        public IActionResult GetLeftSpaceOnHddFromAllCluster()
        {
            return Ok();
        }
    }
}