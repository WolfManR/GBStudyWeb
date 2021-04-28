using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    public class RamMetricsController : ApiController
    {
        [HttpGet("available/agent/{agentId}")]
        public IActionResult GetAvailableSpaceInfoFromAgent([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpGet("available/cluster")]
        public IActionResult GetAvailableSpaceInfoFromAllCluster()
        {
            return Ok();
        }
    }
}