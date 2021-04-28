using System.Collections.Generic;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent.DataBase.Repositories
{
    public interface IRepository<TEntity,TId> where TEntity:IEntity<TId>
    {
        IList<TEntity> Get();
        TEntity Get(TId id);
        TId Create(TEntity entity);
    }
}