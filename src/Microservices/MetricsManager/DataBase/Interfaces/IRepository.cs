using System;
using System.Collections.Generic;

namespace MetricsManager.DataBase.Interfaces
{
    public interface IRepository<TEntity,TId, in TAgentId> where TEntity: IMetricEntity<TId, TAgentId>
    {
        IList<TEntity> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to);
        IList<TEntity> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to, TAgentId agentId);
        void Create(TEntity entity);
        DateTimeOffset GetAgentLastMetricDate(int agentId);
    }
}