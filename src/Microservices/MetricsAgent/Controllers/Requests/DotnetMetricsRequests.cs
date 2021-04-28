using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers.Requests
{
    public record ErrorsCountRequest
    (
        [FromRoute] TimeSpan FromTime,
        [FromRoute] TimeSpan ToTime
    );
}