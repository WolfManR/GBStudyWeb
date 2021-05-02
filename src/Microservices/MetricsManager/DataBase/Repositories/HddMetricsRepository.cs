using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsManager.DataBase.Interfaces;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase.Repositories
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly SQLiteContainer _container;

        
        public HddMetricsRepository(SQLiteContainer container)
        {
            _container = container;
        }
        
        
        /// <inheritdoc />
        public IList<HddMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM hddmetrics WHERE (time > @from) and (time < @to)";
            cmd.Parameters.AddWithValue("@from", fromSeconds);
            cmd.Parameters.AddWithValue("@to", toSeconds);
            cmd.Prepare();
            
            connection.Open();
            var temp = new List<HddMetric>();
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                temp.Add(new()
                {
                    Id = reader.GetInt32(0),
                    AgentId = reader.GetInt32(1),
                    Time = reader.GetInt32(2),
                    Value = reader.GetInt32(3)
                });
            }
            connection.Close();
            return temp.Count > 0 ? temp : null;
        }

        /// <inheritdoc />
        public IList<HddMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to, int agentId)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM hddmetrics WHERE (agentId=@agentId) and (time > @from) and (time < @to)";
            cmd.Parameters.AddWithValue("@from", fromSeconds);
            cmd.Parameters.AddWithValue("@to", toSeconds);
            cmd.Parameters.AddWithValue("@agentId", agentId);
            cmd.Prepare();
            
            connection.Open();
            var temp = new List<HddMetric>();
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                temp.Add(new()
                {
                    Id = reader.GetInt32(0),
                    AgentId = reader.GetInt32(1),
                    Time = reader.GetInt32(2),
                    Value = reader.GetInt32(3)
                });
            }
            connection.Close();
            return temp.Count > 0 ? temp : null;
        }

        /// <inheritdoc />
        public void Create(HddMetric entity)
        {
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO hddmetrics(agentId,time,value) VALUES (@agentId,@time,@value);";
            cmd.Parameters.AddWithValue("@value", entity.Value);
            cmd.Parameters.AddWithValue("@time", entity.Time);
            cmd.Parameters.AddWithValue("@agentId", entity.AgentId);
            cmd.Prepare();
            
            connection.Open();
            var result = cmd.ExecuteNonQuery();
            connection.Close();
            
            if (result > 0)
            {
                // _logger.LogInformation(LogEvents.EntityCreationSuccess,"Cpu metric successfully added to database");
                return;
            }
            
            // _logger.LogError(
            //     LogEvents.EntityCreationFailure,
            //     "Failure to add entity: {Value} {Time} to database",
            //     entity.Value,
            //     entity.Time.ToString("h:mm:ss tt zz")
            // ); 
        }
    }
}