using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers.Requests
{
    public record ErrorsCountRequest
    (
        [FromRoute] DateTimeOffset FromTime,
        [FromRoute] DateTimeOffset ToTime
    );
}