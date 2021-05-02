using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Common;
using Dapper;
using MetricsAgent.DataBase.Interfaces;
using MetricsAgent.DataBase.Models;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.DataBase.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly SQLiteContainer _container;

        
        public CpuMetricsRepository(SQLiteContainer container)
        {
            _container = container;
        }


        /// <inheritdoc />
        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            
            using var connection = _container.CreateConnection();
            IEnumerable<CpuMetric> temp;
            if (fromSeconds == toSeconds)
            {
                temp = connection.Query<CpuMetric>(
                    "SELECT * FROM cpumetrics WHERE (time = @from)",
                    new
                    {
                        from = fromSeconds,
                        to = toSeconds
                    });
            }
            else if (fromSeconds < toSeconds)
            {
                temp = connection.Query<CpuMetric>(
                    "SELECT * FROM cpumetrics WHERE (time > @from) and (time < @to)",
                    new
                    {
                        from = toSeconds,
                        to = fromSeconds
                    });
            }
            else
            {
                temp = connection.Query<CpuMetric>(
                    "SELECT * FROM cpumetrics WHERE (time > @from) and (time < @to)",
                    new
                    {
                        from = fromSeconds,
                        to = toSeconds
                    });
            }

            var byTimePeriod = temp.ToList();
            return byTimePeriod.Count > 0 ? byTimePeriod : null;
        }

        /// <inheritdoc />
        public void Create(CpuMetric entity)
        {
            using var connection = _container.CreateConnection();
            var result = connection.Execute(
                "INSERT INTO cpumetrics(value,time) VALUES (@value,@time);",
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