using Common;

namespace MetricsManager.DataBase.Models
{
    public class AgentInfo : IEntity<int>
    {
        public int Id { get; init; }
        public string Uri { get; init; }
    }
}