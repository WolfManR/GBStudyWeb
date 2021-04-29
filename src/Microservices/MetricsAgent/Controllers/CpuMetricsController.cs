using System.Linq;
using Common;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.Controllers.Responses;
using MetricsAgent.DataBase.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    public class CpuMetricsController : ApiController
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] CpuMetricsRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get cpu metrics by time period request received: {From}, {To}"
                ,request.FromTime.ToString("h:mm:ss tt zz"),
                request.ToTime.ToString("h:mm:ss tt zz"));

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(new CpuMetricsByTimePeriodResponse(){Metrics = result.Select(m => m.Value).ToList()});
        }
    }
}