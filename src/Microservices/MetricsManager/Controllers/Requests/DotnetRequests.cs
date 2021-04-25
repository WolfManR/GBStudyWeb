using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.Requests
{
    public record GetErrorsCountFromAgentRequest(
        [FromRoute] int AgentId,
        [FromRoute] TimeSpan FromTime,
        [FromRoute] TimeSpan ToTime);
    
    public record GetErrorsCountFromAllClusterRequest(
        [FromRoute] TimeSpan FromTime,
        [FromRoute] TimeSpan ToTime);
}