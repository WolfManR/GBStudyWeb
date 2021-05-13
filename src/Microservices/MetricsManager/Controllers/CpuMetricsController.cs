using System.Linq;
using Common;
using MetricsManager.Controllers.Requests;
using MetricsManager.Controllers.Responses;
using MetricsManager.DataBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
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
        
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] CpuMetricsFromAgentRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get cpu metrics by time period request received: {From}, {To}, for agent: {AgentId}",
                request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"),
                request.AgentId);

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime, request.AgentId);
            if (result is null)
            {
                return NotFound();
            }
            
            return Ok(new CpuGetMetricsFromAgentResponse()
            {
                Metrics = result.Select(Mapper.Map<CpuMetricResponse>)
            });
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] CpuMetricsFromAllClusterRequest request)
        {
            _logger.LogInformation(
                LogEvents.RequestReceived,
                "Get cpu metrics by time period request received: {From}, {To}",
                request.FromTime.ToString("yyyy-M-d dddd"),
                request.ToTime.ToString("yyyy-M-d dddd"));

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            if (result is null)
            {
                return NotFound();
            }
            
            return Ok(new CpuGetMetricsFromAllClusterResponse()
            {
                Metrics = result.Select(Mapper.Map<CpuMetricResponse>)
            });
        }
    }
}