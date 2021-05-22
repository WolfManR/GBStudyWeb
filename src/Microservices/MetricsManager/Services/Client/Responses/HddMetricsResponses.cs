using System;
using System.Collections.Generic;

namespace MetricsManager.Services.Client.Responses
{
    public class HddMetricsByTimePeriodResponse
    {
        public IList<HddMetricResponse> Metrics { get; init; }
    }

    public class HddMetricResponse
    {
        public int Value { get; init; }
        public DateTimeOffset Time { get; init; }
    }
}