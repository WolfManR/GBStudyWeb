using System;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.Requests
{
    public record GetCpuMetricsFromAgentRequest(
        [FromRoute] int AgentId, 
        [FromRoute] TimeSpan FromTime, 
        [FromRoute] TimeSpan ToTime);

    public record GetCpuMetricsByPercentileFromAgentRequest(
        [FromRoute] int AgentId, 
        [FromRoute] TimeSpan FromTime, 
        [FromRoute] TimeSpan ToTime,
        [FromRoute] Percentile Percentile);

    public record GetCpuMetricsFromAllCluster(
        [FromRoute] TimeSpan FromTime, 
        [FromRoute] TimeSpan ToTime);
    
    public record GetCpuMetricsByPercentileFromAllCluster(
        [FromRoute] TimeSpan FromTime, 
        [FromRoute] TimeSpan ToTime,
        [FromRoute] Percentile Percentile);
}