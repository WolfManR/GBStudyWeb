using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.Requests
{
    public record NetworkMetricsFromAgentRequest(
        [FromRoute] int AgentId, 
        [FromRoute] TimeSpan FromTime, 
        [FromRoute] TimeSpan ToTime);

    public record NetworkMetricsFromAllClusterRequest(
        [FromRoute] TimeSpan FromTime, 
        [FromRoute] TimeSpan ToTime);
}