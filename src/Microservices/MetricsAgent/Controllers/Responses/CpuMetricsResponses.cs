using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class CpuMetricsByTimePeriodResponse
    {
        public List<int> Metrics { get; init; }
    }
}