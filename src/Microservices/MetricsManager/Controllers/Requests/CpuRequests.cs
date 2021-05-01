using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.Requests
{
    public record CpuMetricsFromAgentRequest(
        [FromRoute] int AgentId, 
        [FromRoute] DateTimeOffset FromTime, 
        [FromRoute] DateTimeOffset ToTime);

    public record CpuMetricsFromAllClusterRequest(
        [FromRoute] DateTimeOffset FromTime, 
        [FromRoute] DateTimeOffset ToTime);
}