using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class RamMetricsByTimePeriodResponse
    {
        public List<int> Metrics { get; init; }
    }
}