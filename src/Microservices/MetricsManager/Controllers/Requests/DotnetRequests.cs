using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.Requests
{
    public record ErrorsCountFromAgentRequest(
        [FromRoute] int AgentId,
        [FromRoute] TimeSpan FromTime,
        [FromRoute] TimeSpan ToTime);
    
    public record ErrorsCountFromAllClusterRequest(
        [FromRoute] TimeSpan FromTime,
        [FromRoute] TimeSpan ToTime);
}