using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class DotnetGetMetricsFromAgentResponse
    {
        public List<DotnetMetricResponse> Metrics { get; init; }
    }

    public class DotnetGetMetricsFromAllClusterResponse
    {
        public List<DotnetMetricResponse> Metrics { get; init; }
    }
    
    public class DotnetMetricResponse{
        public int Id { get; init; }
        public int AgentId { get; init; }
        public DateTimeOffset Time { get; init; }
        public int Value { get; init; }
    }
}