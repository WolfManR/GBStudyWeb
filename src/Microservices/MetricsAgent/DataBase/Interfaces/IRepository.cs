using System;
using System.Collections.Generic;

namespace MetricsAgent.DataBase.Interfaces
{
    public interface IRepository<TEntity,TId> where TEntity:IEntity<TId>
    {
        IList<TEntity> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to);
        void Create(TEntity entity);
    }
}