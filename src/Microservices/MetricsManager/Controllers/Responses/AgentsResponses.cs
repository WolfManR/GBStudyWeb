using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class GetRegisteredAgentsResponse
    {
        public List<GetAgentResponse> Agents { get; init; }
    }

    public class GetAgentResponse
    {
        public int Id { get; init; }
        public string Url { get; init; }
    }
}