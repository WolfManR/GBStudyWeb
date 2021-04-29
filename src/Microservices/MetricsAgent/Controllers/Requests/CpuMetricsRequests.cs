using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers.Requests
{
    public record CpuMetricsRequest
    (
        [FromRoute] DateTimeOffset FromTime,
        [FromRoute] DateTimeOffset ToTime
    );
}