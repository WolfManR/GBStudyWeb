using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class NetworkGetMetricsFromAgentResponse
    {
        public IEnumerable<NetworkMetricResponse> Metrics { get; init; }
    }

    public class NetworkGetMetricsFromAllClusterResponse
    {
        public IEnumerable<NetworkMetricResponse> Metrics { get; init; }
    }
    
    public class NetworkMetricResponse{
        public int Id { get; init; }
        public int AgentId { get; init; }
        public DateTimeOffset Time { get; init; }
        public int Value { get; init; }
    }
}