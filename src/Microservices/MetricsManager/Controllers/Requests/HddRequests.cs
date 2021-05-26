using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.Requests
{
    public record HddMetricsFromAgentRequest(
        [FromRoute] int AgentId, 
        [FromRoute] DateTimeOffset FromTime, 
        [FromRoute] DateTimeOffset ToTime);

    public record HddMetricsFromAllClusterRequest(
        [FromRoute] DateTimeOffset FromTime, 
        [FromRoute] DateTimeOffset ToTime);
}