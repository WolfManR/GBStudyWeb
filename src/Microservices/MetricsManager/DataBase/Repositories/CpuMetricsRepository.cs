using System;
using Common.Configuration;
using Dapper;
using MetricsManager.DataBase.Interfaces;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase.Repositories
{
    public class CpuMetricsRepository : MetricRepository<CpuMetric, int, int>, ICpuMetricsRepository
    {
        public CpuMetricsRepository(SQLiteContainer container) : base(container)
        {
        }
        
        /// <inheritdoc />
        protected override string TableName { get; } = Values.CpuMetricsTable;
        
        /// <inheritdoc />
        public override void Create(CpuMetric entity)
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
                throw new InvalidOperationException("Failure to add cpu metric")
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