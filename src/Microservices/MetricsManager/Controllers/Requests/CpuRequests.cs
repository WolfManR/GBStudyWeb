using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.Requests
{
    public record CpuMetricsFromAgentRequest(
        [FromRoute] int AgentId, 
        [FromRoute] TimeSpan FromTime, 
        [FromRoute] TimeSpan ToTime);

    public record CpuMetricsFromAllClusterRequest(
        [FromRoute] TimeSpan FromTime, 
        [FromRoute] TimeSpan ToTime);
}