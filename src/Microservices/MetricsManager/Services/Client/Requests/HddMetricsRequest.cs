using System;

namespace MetricsManager.Services.Client.Requests
{
    public class HddMetricsRequest
    {
        public string AgentUrl { get; init; }
        public DateTimeOffset FromTime { get; init; }
        public DateTimeOffset ToTime { get; init; }
    }
}