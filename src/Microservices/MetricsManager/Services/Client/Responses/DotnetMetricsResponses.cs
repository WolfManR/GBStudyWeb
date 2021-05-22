using System;
using System.Collections.Generic;

namespace MetricsManager.Services.Client.Responses
{
    public class DotnetMetricsByTimePeriodResponse
    {
        public IList<DotnetMetricResponse> Metrics { get; init; }
    }

    public class DotnetMetricResponse
    {
        public int Value { get; init; }
        public DateTimeOffset Time { get; init; }
    }
}