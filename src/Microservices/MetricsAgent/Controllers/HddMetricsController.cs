using MetricsAgent.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    public class HddMetricsController : ApiController
    {
        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeHardDriveSpace([FromRoute] FreeHardDriveSpaceRequest request)
        {
            return Ok();
        }
    }
}