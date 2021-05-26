using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class HddMetricsByTimePeriodResponse
    {
        public IEnumerable<HddMetricResponse> Metrics { get; init; }
    }

    public class HddMetricResponse
    {
        public int Value { get; init; }
        public DateTimeOffset Time { get; init; }
    }
}