using System.Linq;
using Common;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.Controllers.Responses;
using MetricsAgent.DataBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    public class DotnetMetricsController : ApiController
    {
        private readonly IDotnetMetricsRepository _repository;
        private readonly ILogger<DotnetMetricsController> _logger;

        public DotnetMetricsController(IDotnetMetricsRepository repository, ILogger<DotnetMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCount([FromRoute] ErrorsCountRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get dotnet metrics by time period request received: {From}, {To}"
                ,request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"));

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(new DotnetMetricsByTimePeriodResponse(){Metrics = result.Select(m => m.Value).ToList()});
        }
    }
}