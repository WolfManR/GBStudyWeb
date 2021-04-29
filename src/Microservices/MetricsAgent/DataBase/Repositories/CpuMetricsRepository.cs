using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Common;
using MetricsAgent.DataBase.Models;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.DataBase.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly SQLiteContainer _container;
        private readonly ILogger<CpuMetricsRepository> _logger;

        public CpuMetricsRepository(SQLiteContainer container, ILogger<CpuMetricsRepository> logger)
        {
            _container = container;
            _logger = logger;
        }


        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <inheritdoc />
        public IList<CpuMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM cpumetrics";
            
            connection.Open();
            var temp = new List<CpuMetrics>();
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                temp.Add(new()
                {
                    Id = reader.GetInt32(0),
                    Value = reader.GetInt32(1),
                    Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(2)) 
                });
            }
            connection.Close();
            return temp.Count > 0 ? temp.Where(m => m.Time > from && m.Time < to).ToList() : null;
        }

        /// <inheritdoc />
        public void Create(CpuMetrics entity)
        {
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO cpumetrics(value,time) VALUES (@value,@time);";
            cmd.Parameters.AddWithValue("@value", entity.Value);
            cmd.Parameters.AddWithValue("@time", entity.Time.ToUnixTimeSeconds());
            cmd.Prepare();
            
            connection.Open();
            var result = cmd.ExecuteNonQuery();
            connection.Close();
            
            if (result > 0)
            {
                _logger.LogInformation(LogEvents.EntityCreationSuccess,"Cpu metric successfully added to database");
                return;
            }
            
            _logger.LogError(
                LogEvents.EntityCreationFailure,
                "Failure to add entity: {Value} {Time} to database",
                entity.Value,
                entity.Time.ToString("h:mm:ss tt zz")
                );
        }
    }
}