using System.Linq;
using Common;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.Controllers.Responses;
using MetricsAgent.DataBase.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    public class HddMetricsController : ApiController
    {
        private readonly IHddMetricsRepository _repository;
        private readonly ILogger<HddMetricsController> _logger;

        public HddMetricsController(IHddMetricsRepository repository, ILogger<HddMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeHardDriveSpace([FromRoute] FreeHardDriveSpaceRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get hdd metrics by time period request received: {From}, {To}"
                ,request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"));

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(new HddMetricsByTimePeriodResponse(){Metrics = result.Select(m => m.Value).ToList()});
        }
    }
}