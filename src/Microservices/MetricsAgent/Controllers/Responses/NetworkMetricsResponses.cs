using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class NetworkMetricsByTimePeriodResponse
    {
        public List<int> Metrics { get; init; }
    }
}