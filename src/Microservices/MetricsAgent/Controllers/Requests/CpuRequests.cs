using System;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers.Requests
{
    public record GetCpuMetricsByPercentilesRequest
    (
        [FromRoute(Name = "fromTime")] DateTime FromTime,
        [FromRoute(Name = "toTime")] DateTime ToTime,
        [FromRoute(Name = "percentile")] Percentiles Percentile
    );

    public record GetCpuMetricsRequest
    (
        [FromRoute(Name = "fromTime")] DateTime FromTime,
        [FromRoute(Name = "toTime")] DateTime ToTime
    );
}