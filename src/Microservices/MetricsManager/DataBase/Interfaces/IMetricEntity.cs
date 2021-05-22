using Common;

namespace MetricsManager.DataBase.Interfaces
{
    public interface IMetricEntity<TId,TAgentId> : IEntity<TId>
    {
        public TAgentId AgentId { get; init; }
    }
}