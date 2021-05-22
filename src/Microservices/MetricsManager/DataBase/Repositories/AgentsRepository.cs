using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Common;
using MetricsManager.DataBase.Interfaces;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase.Repositories
{
    public class AgentsRepository : IAgentsRepository
    {
        private readonly SQLiteContainer _container;


        public AgentsRepository(SQLiteContainer container)
        {
            _container = container;
        }


        /// <inheritdoc />
        public void Create(AgentInfo agent)
        {
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            connection.Open();

            cmd.CommandText = "SELECT Count(*) FROM agents WHERE uri=@uri";
            cmd.Parameters.AddWithValue("@uri", agent.Uri);
            var readCount = cmd.ExecuteScalar();
            if (int.TryParse(readCount.ToString(), out var count) && count > 0)
            {
                throw new ArgumentException("Agent already exist") {Data = {["uri"] = agent.Uri}};
            }

            cmd.CommandText = "INSERT INTO agents(uri,isenabled) VALUES (@uri,@isenabled);";
            cmd.Parameters.AddWithValue("@isenabled", agent.IsEnabled);
            var result = cmd.ExecuteNonQuery();

            if (result > 0)
            {
                return;
            }

            throw new InvalidOperationException("Failure to add entity") {Data = {["uri"] = agent.Uri}};
        }

        public void Update(AgentInfo agent)
        {
            using var connection = _container.CreateConnection();
            using var cmd = new SQLiteCommand(connection);
            connection.Open();

            cmd.CommandText = "SELECT Count(*) FROM agents WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", agent.Id);
            var readCount = cmd.ExecuteScalar();
            if (int.TryParse(readCount.ToString(), out var count) && count > 0)
            {
                throw new ArgumentException($"Agent with id: {agent.Id} not exist", nameof(agent))
                {
                    Data =
                    {
                        ["id"] = agent.Id,
                        ["uri"] = agent.Uri,
                        ["isenabled"] = agent.IsEnabled
                    }
                };
            }

            cmd.CommandText = "UPDATE agents SET uri=@uri, isenabled=@isenabled where id=@id;";
            cmd.Parameters.AddWithValue("@uri", agent.Uri);
            cmd.Parameters.AddWithValue("@isenabled", agent.IsEnabled);
            cmd.ExecuteNonQuery();
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
                    Uri = reader.GetString(1),
                    IsEnabled = reader.GetBoolean(2)
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
                    Uri = reader.GetString(1),
                    IsEnabled = reader.GetBoolean(2)
                };
            }

            return null;
        }
    }
}