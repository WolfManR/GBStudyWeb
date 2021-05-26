using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers.Requests
{
    public record AvailableSpaceInfoRequest
    (
        [FromRoute] DateTimeOffset FromTime,
        [FromRoute] DateTimeOffset ToTime
    );
}