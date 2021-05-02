using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class RamGetMetricsFromAgentResponse
    {
        public List<RamMetricResponse> Metrics { get; init; }
    }

    public class RamGetMetricsFromAllClusterResponse
    {
        public List<RamMetricResponse> Metrics { get; init; }
    }
    
    public class RamMetricResponse{
        public int Id { get; init; }
        public int AgentId { get; init; }
        public DateTimeOffset Time { get; init; }
        public int Value { get; init; }
    }
}