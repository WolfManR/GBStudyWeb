using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class HddGetMetricsFromAgentResponse
    {
        public IEnumerable<HddMetricResponse> Metrics { get; init; }
    }

    public class HddGetMetricsFromAllClusterResponse
    {
        public IEnumerable<HddMetricResponse> Metrics { get; init; }
    }
    
    public class HddMetricResponse{
        public int Id { get; init; }
        public int AgentId { get; init; }
        public DateTimeOffset Time { get; init; }
        public int Value { get; init; }
    }
}