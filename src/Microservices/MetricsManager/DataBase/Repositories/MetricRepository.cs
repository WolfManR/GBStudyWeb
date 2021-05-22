using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MetricsManager.DataBase.Interfaces;

namespace MetricsManager.DataBase.Repositories
{
    public abstract class MetricRepository<TEntity,TId, TAgentId> : IRepository<TEntity,TId, TAgentId>
        where TEntity : IMetricEntity<TId, TAgentId>
    {
        protected readonly SQLiteContainer Container;
        
        protected MetricRepository(SQLiteContainer container)
        {
            Container = container;
        }
        
        protected abstract string TableName { get; }
        
        public IList<TEntity> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
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
        
        public IList<TEntity> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to, TAgentId agentId)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            
            using var connection = Container.CreateConnection();
            string command;
            object commandParameters;
            if (fromSeconds == toSeconds)
            {
                command = $"SELECT * FROM {TableName} WHERE (agentId = @agentId) and (time = @from);";
                commandParameters = new { agentId, from = fromSeconds };
            }
            else
            {
                command = $"SELECT * FROM {TableName} WHERE (agentId = @agentId) and (time > @from) and (time < @to);";
                commandParameters = fromSeconds > toSeconds 
                    ? new { agentId, from = toSeconds, to = fromSeconds } 
                    : new { agentId, from = fromSeconds, to = toSeconds };
            }
            
            return connection.Query<TEntity>(command, commandParameters).ToList();
        }
        
        public abstract void Create(TEntity entity);

        public DateTimeOffset GetAgentLastMetricDate(int agentId)
        {
            using var connection = Container.CreateConnection();
            var result = connection.ExecuteScalar<long>(
                $"select Max(time) from {TableName} where agentId = @agentId",
                new { agentId });

            if (result >= 0)
            {
                return DateTimeOffset.FromUnixTimeSeconds(result);
            }

            throw new InvalidOperationException("Failure to get last metric date")
            {
                Data =
                {
                    ["agentId"] = agentId,
                    ["table"] = TableName
                }
            };
        }
    }
}