using System;
using Common.Configuration;
using Dapper;
using MetricsAgent.DataBase.Interfaces;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent.DataBase.Repositories
{
    public class NetworkMetricsRepository : RepositoryBase<NetworkMetric,int>, INetworkMetricsRepository
    {
        public NetworkMetricsRepository(SQLiteContainer container) : base(container)
        {
        }
        
        /// <inheritdoc />
        protected override string TableName { get; } = Values.NetworkMetricsTable;
        
        /// <inheritdoc />
        public override void Create(NetworkMetric entity)
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