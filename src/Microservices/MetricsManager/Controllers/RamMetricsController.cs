using System.Linq;
using AutoMapper;
using Common;
using MetricsManager.Controllers.Requests;
using MetricsManager.Controllers.Responses;
using MetricsManager.DataBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [ApiController]
    [Route("api/metrics/ram")]
    public class RamMetricsController : ControllerBase
    {
        private readonly IRamMetricsRepository _repository;
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IMapper _mapper;

        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get metrics by agent between time range
        /// </summary>
        /// <param name="request">filter that contains agent id and time range</param>
        /// <returns>response that contains list of metrics</returns>
        [HttpGet("available/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] RamMetricsFromAgentRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get ram metrics by time period request received: {From}, {To}, for agent: {AgentId}",
                request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"),
                request.AgentId);

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime, request.AgentId);
            
            return Ok(new RamGetMetricsFromAgentResponse()
            {
                Metrics = result.Select(_mapper.Map<RamMetricResponse>)
            });
        }

        /// <summary>
        /// Get metrics from all registered agents between time range
        /// </summary>
        /// <param name="request">filter that contains time range</param>
        /// <returns>response that contains list of metrics</returns>
        [HttpGet("available/cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] RamMetricsFromAllClusterRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get ram metrics by time period request received: {From}, {To}",
                request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"));

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            
            return Ok(new RamGetMetricsFromAllClusterResponse()
            {
                Metrics = result.Select(_mapper.Map<RamMetricResponse>)
            });
        }
    }
}