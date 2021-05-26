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
    [Route("api/metrics/network")]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IMapper _mapper;

        public NetworkMetricsController(INetworkMetricsRepository repository, ILogger<NetworkMetricsController> logger, IMapper mapper)
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
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] NetworkMetricsFromAgentRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get network metrics by time period request received: {From}, {To}, for agent: {AgentId}",
                request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"),
                request.AgentId);

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime, request.AgentId);
            
            return Ok(new NetworkGetMetricsFromAgentResponse()
            {
                Metrics = result.Select(_mapper.Map<NetworkMetricResponse>)
            });
        }

        /// <summary>
        /// Get metrics from all registered agents between time range
        /// </summary>
        /// <param name="request">filter that contains time range</param>
        /// <returns>response that contains list of metrics</returns>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] NetworkMetricsFromAllClusterRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get network metrics by time period request received: {From}, {To}",
                request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"));

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            
            return Ok(new NetworkGetMetricsFromAllClusterResponse()
            {
                Metrics = result.Select(_mapper.Map<NetworkMetricResponse>)
            });
        }
    }
}