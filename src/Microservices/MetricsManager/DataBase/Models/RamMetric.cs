using Common;

namespace MetricsManager.DataBase.Models
{
    public record RamMetric : IEntity<int>
    {
        public int Id { get; init; }
        public int AgentId { get; init; }
        public long Time { get; init; }
        public int Value { get; init; }
    }
}