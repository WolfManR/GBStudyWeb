using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class RamMetricsByTimePeriodResponse
    {
        public IEnumerable<RamMetricResponse> Metrics { get; init; }
    }

    public class RamMetricResponse
    {
        public int Value { get; init; }
        public DateTimeOffset Time { get; init; }
    }
}