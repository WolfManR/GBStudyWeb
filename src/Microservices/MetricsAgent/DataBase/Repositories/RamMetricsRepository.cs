using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Common;
using MetricsAgent.DataBase.Models;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.DataBase.Repositories
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly SQLiteContainer _container;
        private readonly ILogger<RamMetricsRepository> _logger;

        
        public RamMetricsRepository(SQLiteContainer container, ILogger<RamMetricsRepository> logger)
        {
            _container = container;
            _logger = logger;
        }
        
        
        /// <inheritdoc />
        public IList<RamMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM rammetrics WHERE (time > @from) and (time < @to)";
            cmd.Parameters.AddWithValue("@from", fromSeconds);
            cmd.Parameters.AddWithValue("@to", toSeconds);
            cmd.Prepare();
            
            connection.Open();
            var temp = new List<RamMetric>();
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                temp.Add(new()
                {
                    Id = reader.GetInt32(0),
                    Value = reader.GetInt32(1),
                    Time = reader.GetInt32(2) 
                });
            }
            connection.Close();
            return temp.Count > 0 ? temp : null;
        }

        /// <inheritdoc />
        public void Create(RamMetric entity)
        {
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO rammetrics(value,time) VALUES (@value,@time);";
            cmd.Parameters.AddWithValue("@value", entity.Value);
            cmd.Parameters.AddWithValue("@time", entity.Time);
            cmd.Prepare();
            
            connection.Open();
            var result = cmd.ExecuteNonQuery();
            connection.Close();
            
            if (result > 0)
            {
                _logger.LogInformation(LogEvents.EntityCreationSuccess,"Ram metric successfully added to database");
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