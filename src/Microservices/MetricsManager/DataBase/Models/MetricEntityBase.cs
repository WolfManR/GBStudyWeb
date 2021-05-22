using MetricsManager.DataBase.Interfaces;

namespace MetricsManager.DataBase.Models
{
    public abstract record MetricEntityBase : IMetricEntity<int,int>
    {
        public int Id { get; init; }
        public int AgentId { get; init; }
        public long Time { get; init; }
    }
}