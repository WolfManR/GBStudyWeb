using System;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.Requests
{
    public record GetNetworkMetricsFromAgentRequest(
        [FromRoute] int AgentId, 
        [FromRoute] TimeSpan FromTime, 
        [FromRoute] TimeSpan ToTime);

    public record GetNetworkMetricsFromAllClusterRequest(
        [FromRoute] TimeSpan FromTime, 
        [FromRoute] TimeSpan ToTime);
}