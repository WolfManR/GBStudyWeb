using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class DotnetMetricsByTimePeriodResponse
    {
        public IEnumerable<DotnetMetricResponse> Metrics { get; init; }
    }

    public class DotnetMetricResponse
    {
        public int Value { get; init; }
        public DateTimeOffset Time { get; init; }
    }
}