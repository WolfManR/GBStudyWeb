using System;
using Common.Configuration;
using Dapper;
using MetricsManager.DataBase.Interfaces;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase.Repositories
{
    public class HddMetricsRepository : MetricRepository<HddMetric, int, int>, IHddMetricsRepository
    {
        public HddMetricsRepository(SQLiteContainer container) : base(container)
        {
        }
        
        protected override string TableName { get; } = Values.HddMetricsTable;
        
        public override void Create(HddMetric entity)
        {
            using var connection = Container.CreateConnection();
            var result = connection.Execute(
                $"INSERT INTO {TableName}(agentId,time,value) VALUES (@agentId,@time,@value);",
                new
                {
                    agentId = entity.AgentId,
                    time = entity.Time,
                    value = entity.Value,
                }
            );

            if (result <= 0)
            {
                throw new InvalidOperationException("Failure to add hdd metric")
                {
                    Data =
                    {
                        ["agentId"] = entity.AgentId,
                        ["time"] = entity.Time,
                        ["value"] = entity.Value,
                    }
                };
            }
        }
    }
}