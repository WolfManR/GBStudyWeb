using System;
using System.Collections.Generic;

namespace MetricsManager.Services.Client.Responses
{
    public class CpuMetricsByTimePeriodResponse
    {
        public IList<CpuMetricResponse> Metrics { get; init; }
    }

    public class CpuMetricResponse
    {
        public int Value { get; init; }
        public DateTimeOffset Time { get; init; }
    }
}