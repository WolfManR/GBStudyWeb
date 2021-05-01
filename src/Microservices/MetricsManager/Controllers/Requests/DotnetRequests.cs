using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.Requests
{
    public record ErrorsCountFromAgentRequest(
        [FromRoute] int AgentId,
        [FromRoute] DateTimeOffset FromTime,
        [FromRoute] DateTimeOffset ToTime);
    
    public record ErrorsCountFromAllClusterRequest(
        [FromRoute] DateTimeOffset FromTime,
        [FromRoute] DateTimeOffset ToTime);
}