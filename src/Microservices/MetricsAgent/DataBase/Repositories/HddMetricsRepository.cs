using System;
using Common.Configuration;
using Dapper;
using MetricsAgent.DataBase.Interfaces;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent.DataBase.Repositories
{
    public class HddMetricsRepository : RepositoryBase<HddMetric,int>,IHddMetricsRepository
    {
        public HddMetricsRepository(SQLiteContainer container) : base(container)
        {
        }
        
        protected override string TableName { get; } = Values.HddMetricsTable;
        
        public override void Create(HddMetric entity)
        {
            using var connection = Container.CreateConnection();
            var result = connection.Execute(
                $"INSERT INTO {TableName}(value,time) VALUES (@value,@time);",
                new
                {
                    value = entity.Value,
                    time = entity.Time
                }
            );
            
            if (result <= 0)
            {
                throw new InvalidOperationException("Failure to add entity to database")
                {
                    Data =
                    {
                        ["value"] = entity.Value,
                        ["time"] = entity.Time
                    }
                };
            }
        }
    }
}