using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.Requests
{
    public record RamMetricsFromAgentRequest(
        [FromRoute] int AgentId, 
        [FromRoute] DateTimeOffset FromTime, 
        [FromRoute] DateTimeOffset ToTime);

    public record RamMetricsFromAllClusterRequest(
        [FromRoute] DateTimeOffset FromTime, 
        [FromRoute] DateTimeOffset ToTime);
}