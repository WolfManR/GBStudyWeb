using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public record DotnetMetricsByTimePeriodResponse
    {
        public List<int> Metrics { get; init; }
    }
}