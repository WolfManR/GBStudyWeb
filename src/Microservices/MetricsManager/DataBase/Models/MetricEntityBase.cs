using MetricsManager.DataBase.Interfaces;

namespace MetricsManager.DataBase.Models
{
    public abstract record MetricEntityBase<TId> : IMetricEntity<TId,int>
    {
        public TId Id { get; init; }
        public int AgentId { get; init; }
        public long Time { get; init; }
    }
}