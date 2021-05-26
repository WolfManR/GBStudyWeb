using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class NetworkMetricsByTimePeriodResponse
    {
        public IEnumerable<NetworkMetricResponse> Metrics { get; init; }
    }

    public class NetworkMetricResponse
    {
        public int Value { get; init; }
        public DateTimeOffset Time { get; init; }
    }
}