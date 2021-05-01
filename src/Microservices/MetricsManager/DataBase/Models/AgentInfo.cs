using Common;

namespace MetricsManager.DataBase.Models
{
    public record AgentInfo : IEntity<int>
    {
        public int Id { get; init; }
        public string Uri { get; init; }

        public bool IsEnabled { get; set; }
    }
}