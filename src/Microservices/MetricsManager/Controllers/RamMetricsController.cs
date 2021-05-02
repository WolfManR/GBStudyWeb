using System;
using System.Linq;
using Common;
using MetricsManager.Controllers.Requests;
using MetricsManager.Controllers.Responses;
using MetricsManager.DataBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    public class RamMetricsController : ApiController
    {
        private readonly IRamMetricsRepository _repository;
        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        
        [HttpGet("available/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableSpaceInfoFromAgent([FromRoute] RamMetricsFromAgentRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get ram metrics by time period request received: {From}, {To}, for agent: {AgentId}"
                ,request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"),
                request.AgentId);

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime, request.AgentId);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(new RamGetMetricsFromAgentResponse(){Metrics = result.Select(m =>new RamMetricResponse()
            {
                Id = m.Id,
                Time = DateTimeOffset.FromUnixTimeSeconds(m.Time),
                Value = m.Value,
                AgentId = m.AgentId
            }).ToList()});
        }

        [HttpGet("available/cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableSpaceInfoFromAllCluster([FromRoute] RamMetricsFromAllClusterRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get ram metrics by time period request received: {From}, {To}"
                ,request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"));

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(new RamGetMetricsFromAllClusterResponse(){Metrics = result.Select(m =>new RamMetricResponse()
            {
                Id = m.Id,
                Time = DateTimeOffset.FromUnixTimeSeconds(m.Time),
                Value = m.Value,
                AgentId = m.AgentId
            }).ToList()});
        }
    }
}