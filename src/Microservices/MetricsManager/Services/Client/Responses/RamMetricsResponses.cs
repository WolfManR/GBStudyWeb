using System;
using System.Collections.Generic;

namespace MetricsManager.Services.Client.Responses
{
    public class RamMetricsByTimePeriodResponse
    {
        public IList<RamMetricResponse> Metrics { get; init; }
    }

    public class RamMetricResponse
    {
        public int Value { get; init; }
        public DateTimeOffset Time { get; init; }
    }
}