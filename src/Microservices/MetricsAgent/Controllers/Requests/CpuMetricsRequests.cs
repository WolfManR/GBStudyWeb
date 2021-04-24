using System;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers.Requests
{
    public record GetCpuMetricsByPercentilesRequest
    (
        [FromRoute(Name = "fromTime")] TimeSpan FromTime,
        [FromRoute(Name = "toTime")] TimeSpan ToTime,
        [FromRoute(Name = "percentile")] Percentile Percentile
    );

    public record GetCpuMetricsRequest
    (
        [FromRoute(Name = "fromTime")] TimeSpan FromTime,
        [FromRoute(Name = "toTime")] TimeSpan ToTime
    );
}