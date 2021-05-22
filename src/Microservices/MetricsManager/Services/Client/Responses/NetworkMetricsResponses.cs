using System;
using System.Collections.Generic;

namespace MetricsManager.Services.Client.Responses
{
    public class NetworkMetricsByTimePeriodResponse
    {
        public IList<NetworkMetricResponse> Metrics { get; init; }
    }

    public class NetworkMetricResponse
    {
        public int Value { get; init; }
        public DateTimeOffset Time { get; init; }
    }
}