using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class DotnetMetricsByTimePeriodResponse
    {
        public List<int> Metrics { get; init; }
    }
}