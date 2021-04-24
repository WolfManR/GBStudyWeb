using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers.Requests
{
    public record GetDotnetMetricsRequest
    (
        [FromRoute(Name = "fromTime")] TimeSpan FromTime,
        [FromRoute(Name = "toTime")] TimeSpan ToTime
    );
}