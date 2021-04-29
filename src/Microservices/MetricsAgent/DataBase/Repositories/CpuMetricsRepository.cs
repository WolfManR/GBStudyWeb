using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
        
        
        /// <inheritdoc />
        public IList<CpuMetrics> Get()
        {
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM cpumetrics";
            
            var temp = new List<CpuMetrics>();
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                temp.Add(new()
                {
                    Id = reader.GetInt32(0),
                    Value = reader.GetInt32(1),
                    Time = TimeSpan.FromSeconds(reader.GetInt32(2)) 
                });
            }

            return temp;
        }

        /// <inheritdoc />
        public CpuMetrics Get(int id)
        {
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM cpumetrics WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new()
                {
                    Id = reader.GetInt32(0),
                    Value = reader.GetInt32(1),
                    Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                };
            }
            
            return null;
        }

        /// <inheritdoc />
        public int Create(CpuMetrics entity)
        {
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO cpumetrics(value,time) VALUES (@value,@time); SELECT last_insert_rowid();";
            cmd.Parameters.AddWithValue("@value", entity.Value);
            cmd.Parameters.AddWithValue("@time", entity.Time.TotalSeconds);
            cmd.Prepare();
            var result = cmd.ExecuteScalar();
            
            if (result is long id and < int.MaxValue)
            {
                _logger.LogInformation(LogEvents.EntityCreationSuccess,"Cpu metric successfully added to database");
                return (int) id;
            }
            
            _logger.LogError(
                LogEvents.EntityCreationFailure,
                "Can't get entity id from database, returned value: {Value} larger than expected: {Expected}",
                result.ToString(),
                int.MaxValue
                );
            return -1;
        }
    }
}