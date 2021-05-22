using System.Linq;
using AutoMapper;
using Common;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.Controllers.Responses;
using MetricsAgent.DataBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [ApiController]
    [Route("api/metrics/hdd")]
    public class HddMetricsController : ControllerBase
    {
        private readonly IHddMetricsRepository _repository;
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMapper _mapper;

        public HddMetricsController(IHddMetricsRepository repository, ILogger<HddMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get hdd metrics by time period
        /// </summary>
        /// <param name="request">Request that hold time period filter</param>
        /// <returns>List of metrics that have been saved over a given time range</returns>
        /// <response code="200">if metrics found</response>
        /// <response code="404">if metrics not found</response>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetByTimePeriod([FromRoute] FreeHardDriveSpaceRequest request)
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
            return Ok(new HddMetricsByTimePeriodResponse()
            {
                Metrics = result.Select(_mapper.Map<HddMetricResponse>)
            });
        }
    }
}