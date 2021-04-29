using System;
using System.Collections.Generic;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent.DataBase.Repositories
{
    public interface IRepository<TEntity,TId> where TEntity:IEntity<TId>
    {
        IList<TEntity> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to);
        void Create(TEntity entity);
    }
}