using System.Data;
using Bogus;
using Dapper;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase
{
    public class SQLiteInitializer
    {
        private readonly SQLiteContainer _container;
        private const int AgentsCount = 4;

        private Faker<AgentInfo> agentsGenerator = new Faker<AgentInfo>().CustomInstantiator(f => new()
        {
            Id = f.IndexFaker,
            Uri = f.Internet.Url(),
            IsEnabled = f.Random.Bool()
        });

        private Faker<CpuMetric> cpuMetricsGenerator = new Faker<CpuMetric>().CustomInstantiator(f => new()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });

        private Faker<DotnetMetric> dotnetMetricsGenerator = new Faker<DotnetMetric>().CustomInstantiator(f => new()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });

        private Faker<HddMetric> hddMetricsGenerator = new Faker<HddMetric>().CustomInstantiator(f => new()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });

        private Faker<NetworkMetric> networkMetricsGenerator = new Faker<NetworkMetric>().CustomInstantiator(f => new()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });

        private Faker<RamMetric> ramMetricsGenerator = new Faker<RamMetric>().CustomInstantiator(f => new()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });


        public SQLiteInitializer(SQLiteContainer container)
        {
            _container = container;
        }


        public void Init()
        {
            using var connection = _container.CreateConnection();

            // AGENTS
            RecreateTable(
                connection,
                "agents",
                "id INTEGER PRIMARY KEY, uri text, isenabled INT NOT NULL"
            );
            foreach (var agent in agentsGenerator.Generate(AgentsCount))
            {
                AddAgent(connection, agent);
            }

            // CPU
            RecreateTable(
                connection,
                "cpumetrics",
                "id INTEGER PRIMARY KEY, agentId INT NOT NULL, time INT, value INT"
            );
            foreach (var cpu in cpuMetricsGenerator.Generate(10))
            {
                AddCpuEntry(connection, cpu);
            }

            // DOTNET
            RecreateTable(
                connection,
                "dotnetmetrics",
                "id INTEGER PRIMARY KEY, agentId INT NOT NULL, time INT, value INT"
            );
            foreach (var dotnet in dotnetMetricsGenerator.Generate(10))
            {
                AddDotnetEntry(connection, dotnet);
            }

            // HDD
            RecreateTable(
                connection,
                "hddmetrics",
                "id INTEGER PRIMARY KEY, agentId INT NOT NULL, time INT, value INT"
            );
            foreach (var hdd in hddMetricsGenerator.Generate(10))
            {
                AddHddEntry(connection, hdd);
            }

            // NETWORK
            RecreateTable(
                connection,
                "networkmetrics",
                "id INTEGER PRIMARY KEY, agentId INT NOT NULL, time INT, value INT"
            );
            foreach (var network in networkMetricsGenerator.Generate(10))
            {
                AddNetworkEntry(connection, network);
            }

            //RAM
            RecreateTable(
                connection,
                "rammetrics",
                "id INTEGER PRIMARY KEY, agentId INT NOT NULL, time INT, value INT"
            );
            foreach (var ram in ramMetricsGenerator.Generate(10))
            {
                AddRamEntry(connection, ram);
            }
        }


        private void RecreateTable(IDbConnection connection, string tableName, string tableEntrySchema)
        {
            connection.Execute($"DROP TABLE IF EXISTS {tableName}");
            connection.Execute($"CREATE TABLE {tableName}({tableEntrySchema})");
        }

        private void AddAgent(IDbConnection connection, AgentInfo agent)
        {
            connection.Execute(
                "INSERT INTO agents(uri,isenabled) VALUES (@uri,@isenabled);",
                new
                {
                    uri = agent.Uri,
                    isenabled = agent.IsEnabled
                });
        }

        private void AddCpuEntry(IDbConnection connection, CpuMetric metric)
        {
            connection.Execute(
                "INSERT INTO cpumetrics(agentId,time,value) VALUES (@agentId,@time,@value);",
                new
                {
                    agentId = metric.AgentId,
                    time = metric.Time,
                    value = metric.Value
                });
        }

        private void AddDotnetEntry(IDbConnection connection, DotnetMetric metric)
        {
            connection.Execute(
                "INSERT INTO dotnetmetrics(agentId,time,value) VALUES (@agentId,@time,@value);",
                new
                {
                    agentId = metric.AgentId,
                    time = metric.Time,
                    value = metric.Value
                });
        }

        private void AddHddEntry(IDbConnection connection, HddMetric metric)
        {
            connection.Execute(
                "INSERT INTO hddmetrics(agentId,time,value) VALUES (@agentId,@time,@value);",
                new
                {
                    agentId = metric.AgentId,
                    time = metric.Time,
                    value = metric.Value
                });
        }

        private void AddNetworkEntry(IDbConnection connection, NetworkMetric metric)
        {
            connection.Execute(
                "INSERT INTO networkmetrics(agentId,time,value) VALUES (@agentId,@time,@value);",
                new
                {
                    agentId = metric.AgentId,
                    time = metric.Time,
                    value = metric.Value
                });
        }

        private void AddRamEntry(IDbConnection connection, RamMetric metric)
        {
            connection.Execute(
                "INSERT INTO rammetrics(agentId,time,value) VALUES (@agentId,@time,@value);",
                new
                {
                    agentId = metric.AgentId,
                    time = metric.Time,
                    value = metric.Value
                });
        }
    }
}