using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class CpuGetMetricsFromAgentResponse
    {
        public IEnumerable<CpuMetricResponse> Metrics { get; init; }
    }

    public class CpuGetMetricsFromAllClusterResponse
    {
        public IEnumerable<CpuMetricResponse> Metrics { get; init; }
    }
    
    public class CpuMetricResponse{
        public int Id { get; init; }
        public int AgentId { get; init; }
        public DateTimeOffset Time { get; init; }
        public int Value { get; init; }
    }
}