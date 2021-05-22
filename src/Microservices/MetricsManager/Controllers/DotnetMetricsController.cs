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
        
        [HttpGet("errors-count/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountFromAgent([FromRoute] ErrorsCountFromAgentRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get dotnet metrics by time period request received: {From}, {To}, for agent: {AgentId}"
                ,request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"),
                request.AgentId);

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime, request.AgentId);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(new DotnetGetMetricsFromAgentResponse(){Metrics = result.Select(m =>new DotnetMetricResponse()
            {
                Id = m.Id,
                Time = DateTimeOffset.FromUnixTimeSeconds(m.Time),
                Value = m.Value,
                AgentId = m.AgentId
            }).ToList()});
        }

        [HttpGet("errors-count/cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountFromAllCluster([FromRoute] ErrorsCountFromAllClusterRequest request)
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
            return Ok(new DotnetGetMetricsFromAllClusterResponse(){Metrics = result.Select(m =>new DotnetMetricResponse()
            {
                Id = m.Id,
                Time = DateTimeOffset.FromUnixTimeSeconds(m.Time),
                Value = m.Value,
                AgentId = m.AgentId
            }).ToList()});
        }
    }
}