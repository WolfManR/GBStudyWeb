using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    public class DotnetController : ApiController
    {
        [HttpGet("errors-count/from/{fromTime:datetime}/to/{toTime:datetime}")]
        public IActionResult GetErrorsCount([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            return Ok();
        }
    }
}