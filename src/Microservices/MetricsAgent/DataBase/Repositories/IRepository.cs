using System.Collections.Generic;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent.DataBase.Repositories
{
    public interface IRepository<TEntity,TScu> where TEntity:IEntity<TScu>
    {
        IList<TEntity> Get();
        TEntity Get(TScu scu);
        TScu Create(TEntity entity);
    }
}