using System.Collections.Generic;
using System.Data.SQLite;
using Common;
using MetricsManager.DataBase.Interfaces;
using MetricsManager.DataBase.Models;
using Microsoft.Extensions.Logging;

namespace MetricsManager.DataBase.Repositories
{
    public class AgentsRepository : IAgentsRepository
    {
        private readonly SQLiteContainer _container;
        private readonly ILogger<AgentsRepository> _logger;

        
        public AgentsRepository(SQLiteContainer container, ILogger<AgentsRepository> logger)
        {
            _container = container;
            _logger = logger;
        }
        
        
        /// <inheritdoc />
        public void Create(AgentInfo agent)
        {
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            connection.Open();

            cmd.CommandText = "SELECT Count(*) FROM agents WHERE uri=@uri";
            cmd.Parameters.AddWithValue("@uri", agent.Uri);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var count = reader.GetInt32(0);
                if (count > 0)
                {
                    _logger.LogError(
                        LogEvents.EntityCreationFailure,
                        "Failure to add entity: {Uri} to database, entity already exist",
                        agent.Uri);
                    return;
                }
            }

            cmd.CommandText = "INSERT INTO agents(uri) VALUES (@uri);";
            var result = cmd.ExecuteNonQuery();
            
            if (result > 0)
            {
                _logger.LogInformation(LogEvents.EntityCreationSuccess,"Agent successfully added to database");
                return;
            }
            
            _logger.LogError(
                LogEvents.EntityCreationFailure,
                "Failure to add entity: {Uri} to database",
                agent.Uri);
        }

        /// <inheritdoc />
        public IList<AgentInfo> Get()
        {
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM agents";
            connection.Open();
            
            var temp = new List<AgentInfo>();
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                temp.Add(new()
                {
                    Id = reader.GetInt32(0),
                    Uri = reader.GetString(1)
                });
            }
            return temp.Count > 0 ? temp : null;
        }

        /// <inheritdoc />
        public AgentInfo GetById(int id)
        {
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM agents WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            using var reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                return new()
                {
                    Id = reader.GetInt32(0),
                    Uri = reader.GetString(1)
                };
            }
            
            return  null;
        }
    }
}