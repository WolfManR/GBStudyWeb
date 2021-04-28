using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers.Requests
{
    public record CpuMetricsRequest
    (
        [FromRoute] TimeSpan FromTime,
        [FromRoute] TimeSpan ToTime
    );
}