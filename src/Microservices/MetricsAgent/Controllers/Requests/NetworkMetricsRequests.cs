using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers.Requests
{
    public record NetworkMetricsRequest
    (
        [FromRoute] TimeSpan FromTime,
        [FromRoute] TimeSpan ToTime
    );
}