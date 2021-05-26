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
    [Route("api/metrics/cpu")]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMapper _mapper;

        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get cpu metrics by time period
        /// </summary>
        /// <param name="request">Request that hold time period filter</param>
        /// <returns>List of metrics that have been saved over a given time range</returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public CpuMetricsByTimePeriodResponse GetByTimePeriod([FromRoute] CpuMetricsRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get cpu metrics by time period request received: {From}, {To}"
                ,request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"));

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            return new() { Metrics = result.Select(_mapper.Map<CpuMetricResponse>) };
        }
    }
}