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
        
        
        [HttpGet("left/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetLeftSpaceOnHddFromAgent([FromRoute] HddMetricsFromAgentRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get hdd metrics by time period request received: {From}, {To}, for agent: {AgentId}"
                ,request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"),
                request.AgentId);

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime, request.AgentId);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(new HddGetMetricsFromAgentResponse(){Metrics = result.Select(m =>new HddMetricResponse()
            {
                Id = m.Id,
                Time = DateTimeOffset.FromUnixTimeSeconds(m.Time),
                Value = m.Value,
                AgentId = m.AgentId
            }).ToList()});
        }

        [HttpGet("left/cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetLeftSpaceOnHddFromAllCluster([FromRoute] HddMetricsFromAllClusterRequest request)
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
            return Ok(new HddGetMetricsFromAllClusterResponse(){Metrics = result.Select(m =>new HddMetricResponse()
            {
                Id = m.Id,
                Time = DateTimeOffset.FromUnixTimeSeconds(m.Time),
                Value = m.Value,
                AgentId = m.AgentId
            }).ToList()});
        }
    }
}