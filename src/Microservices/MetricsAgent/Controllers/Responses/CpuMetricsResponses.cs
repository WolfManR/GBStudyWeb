using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class CpuMetricsByTimePeriodResponse
    {
        public IEnumerable<CpuMetricResponse> Metrics { get; init; }
    }

    public class CpuMetricResponse
    {
        public int Value { get; init; }
        public DateTimeOffset Time { get; init; }
    }
}