using System;
using Common.Configuration;
using Dapper;
using MetricsManager.DataBase.Interfaces;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase.Repositories
{
    public class DotnetMetricsRepository : MetricRepository<DotnetMetric, int, int>, IDotnetMetricsRepository
    {
        public DotnetMetricsRepository(SQLiteContainer container) : base(container)
        {
        }
        
        /// <inheritdoc />
        protected override string TableName { get; } = Values.DotnetMetricsTable;
        
        /// <inheritdoc />
        public override void Create(DotnetMetric entity)
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
                throw new InvalidOperationException("Failure to add dotnet metric")
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