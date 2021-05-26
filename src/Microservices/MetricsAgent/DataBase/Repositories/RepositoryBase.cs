using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Dapper;
using MetricsAgent.DataBase.Interfaces;

namespace MetricsAgent.DataBase.Repositories
{
    public abstract class RepositoryBase<TEntity,TId> : IRepository<TEntity,TId> where TEntity:IEntity<TId>
    {
        protected abstract string TableName { get; }
        protected readonly SQLiteContainer Container;
        
        protected RepositoryBase(SQLiteContainer container)
        {
            Container = container;
        }
        
        public virtual IList<TEntity> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            
            using var connection = Container.CreateConnection();
            string command;
            object commandParameters;
            if (fromSeconds == toSeconds)
            {
                command = $"SELECT * FROM {TableName} WHERE (time = @from);";
                commandParameters = new { from = fromSeconds };
            }
            else
            {
                command = $"SELECT * FROM {TableName} WHERE (time > @from) and (time < @to);";
                commandParameters = fromSeconds > toSeconds 
                    ? new { from = toSeconds, to = fromSeconds } 
                    : new { from = fromSeconds, to = toSeconds };
            }
            
            return connection.Query<TEntity>(command, commandParameters).ToList();
        }
        
        public abstract void Create(TEntity entity);
    }
}