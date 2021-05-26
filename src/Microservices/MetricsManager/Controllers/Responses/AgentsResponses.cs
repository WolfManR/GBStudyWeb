using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class GetRegisteredAgentsResponse
    {
        public IEnumerable<GetAgentResponse> Agents { get; init; }
    }

    public class GetAgentResponse
    {
        public int Id { get; init; }
        public string Uri { get; init; }
        public bool IsEnabled { get; init; }
    }
}