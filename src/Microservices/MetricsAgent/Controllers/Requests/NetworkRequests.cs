using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers.Requests
{
    public record GetNetworkMetricsRequest
    (
        [FromRoute(Name = "fromTime")] DateTime FromTime,
        [FromRoute(Name = "toTime")] DateTime ToTime
    );
}