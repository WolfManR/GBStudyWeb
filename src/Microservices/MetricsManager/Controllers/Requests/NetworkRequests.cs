using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.Requests
{
    public record NetworkMetricsFromAgentRequest(
        [FromRoute] int AgentId, 
        [FromRoute] DateTimeOffset FromTime, 
        [FromRoute] DateTimeOffset ToTime);

    public record NetworkMetricsFromAllClusterRequest(
        [FromRoute] DateTimeOffset FromTime, 
        [FromRoute] DateTimeOffset ToTime);
}