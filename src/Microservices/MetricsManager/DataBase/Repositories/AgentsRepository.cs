using System;
using System.Collections.Generic;
using System.Linq;
using Common.Configuration;
using Dapper;
using MetricsManager.DataBase.Interfaces;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase.Repositories
{
    public class AgentsRepository : IAgentsRepository
    {
        private readonly SQLiteContainer _container;
        private const string TableName = Values.AgentsMetricsTable;

        public AgentsRepository(SQLiteContainer container)
        {
            _container = container;
        }
        
        /// <inheritdoc />
        public void Create(AgentInfo agent)
        {
            using var connection = _container.CreateConnection();

            var count = connection.ExecuteScalar<int>($"SELECT Count(*) FROM {TableName} WHERE uri=@uri;", new { uri = agent.Uri });
            if (count > 0)
            {
                throw new ArgumentException("Agent already exist") {Data = {["uri"] = agent.Uri}};
            }

            var result = connection.Execute(
                $"INSERT INTO {TableName}(uri,isenabled) VALUES (@uri,@isenabled);",
                new { uri = agent.Uri, isenabled = agent.IsEnabled }
                );
            

            if (result <= 0)
            {
                throw new InvalidOperationException("Failure to add agent") {Data = {["uri"] = agent.Uri}};
            }
        }

        /// <inheritdoc />
        public void Update(AgentInfo agent)
        {
            using var connection = _container.CreateConnection();

            var count = connection.ExecuteScalar<int>(
                $"SELECT Count(*) FROM {TableName} WHERE id=@id",
                new { id = agent.Id }
                );
            if (count > 0)
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

            var result = connection.Execute(
                $"UPDATE {TableName} SET uri=@uri, isenabled=@isenabled where id=@id;",
                new { uri = agent.Uri, isenabled = agent.IsEnabled }
                );

            if (result <= 0)
            {
                throw new InvalidOperationException("Failure to update agent")
                {
                    Data =
                    {
                        ["id"] = agent.Id,
                        ["uri"] = agent.Uri,
                        ["isenabled"] = agent.IsEnabled
                    }
                };
            }
        }

        /// <inheritdoc />
        public IList<AgentInfo> Get()
        {
            using var connection = _container.CreateConnection();
            var temp = connection.Query<AgentInfo>($"SELECT * FROM {TableName}").ToList();
            return temp.Count > 0 ? temp : null;
        }

        /// <inheritdoc />
        public AgentInfo GetById(int id)
        {
            using var connection = _container.CreateConnection();
            return connection.QuerySingle<AgentInfo>($"SELECT * FROM {TableName} WHERE id=@id", new { id });
        }
    }
}