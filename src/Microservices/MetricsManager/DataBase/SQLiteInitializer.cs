using System.Data;
using Bogus;
using Common.Configuration;
using Dapper;
using FluentMigrator.Runner;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase
{
    public class SQLiteInitializer
    {
        private readonly SQLiteContainer _container;
        private readonly IMigrationRunner _migrationRunner;
        private const int AgentsCount = 4;

        private readonly Faker<AgentInfo> _agentsGenerator = new Faker<AgentInfo>().CustomInstantiator(f => new()
        {
            Id = f.IndexFaker,
            Uri = f.Internet.Url(),
            IsEnabled = f.Random.Bool()
        });

        private readonly Faker<CpuMetric> _cpuMetricsGenerator = new Faker<CpuMetric>().CustomInstantiator(f => new()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });

        private readonly Faker<DotnetMetric> _dotnetMetricsGenerator = new Faker<DotnetMetric>().CustomInstantiator(f => new()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });

        private readonly Faker<HddMetric> _hddMetricsGenerator = new Faker<HddMetric>().CustomInstantiator(f => new()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });

        private readonly Faker<NetworkMetric> _networkMetricsGenerator = new Faker<NetworkMetric>().CustomInstantiator(f => new()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });

        private readonly Faker<RamMetric> _ramMetricsGenerator = new Faker<RamMetric>().CustomInstantiator(f => new()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });
        
        public SQLiteInitializer(SQLiteContainer container, IMigrationRunner migrationRunner)
        {
            _container = container;
            _migrationRunner = migrationRunner;
        }
        
        public void Init()
        {
            _migrationRunner.MigrateUp();

            using var connection = _container.CreateConnection();

            // AGENTS
            if (IsTableNotFilled(connection, Values.CpuMetricsTable))
                foreach (var agent in _agentsGenerator.Generate(AgentsCount))
                    AddAgent(connection, agent);

            // CPU
            if (IsTableNotFilled(connection, Values.CpuMetricsTable))
                foreach (var cpu in _cpuMetricsGenerator.Generate(10))
                    AddCpuEntry(connection, cpu);

            // DOTNET
            if (IsTableNotFilled(connection, Values.DotnetMetricsTable))
                foreach (var dotnet in _dotnetMetricsGenerator.Generate(10))
                    AddDotnetEntry(connection, dotnet);

            // HDD
            if (IsTableNotFilled(connection, Values.HddMetricsTable))
                foreach (var hdd in _hddMetricsGenerator.Generate(10))
                    AddHddEntry(connection, hdd);

            // NETWORK
            if (IsTableNotFilled(connection, Values.NetworkMetricsTable))
                foreach (var network in _networkMetricsGenerator.Generate(10))
                    AddNetworkEntry(connection, network);

            //RAM
            if (IsTableNotFilled(connection, Values.RamMetricsTable))
                foreach (var ram in _ramMetricsGenerator.Generate(10))
                    AddRamEntry(connection, ram);
        }
        
        private static bool IsTableNotFilled(IDbConnection connection, string tableName)
        {
            var count = connection.ExecuteScalar<int>($"SELECT Count(*) FROM {tableName};");
            return count <= 0;
        }

        private static void AddAgent(IDbConnection connection, AgentInfo agent)
        {
            connection.Execute(
                $"INSERT INTO {Values.AgentsMetricsTable}(uri,isenabled) VALUES (@uri,@isenabled);",
                new
                {
                    uri = agent.Uri,
                    isenabled = agent.IsEnabled
                });
        }

        private static void AddCpuEntry(IDbConnection connection, CpuMetric metric)
        {
            connection.Execute(
                $"INSERT INTO {Values.CpuMetricsTable}(agentId,time,value) VALUES (@agentId,@time,@value);",
                new
                {
                    agentId = metric.AgentId,
                    time = metric.Time,
                    value = metric.Value
                });
        }

        private static void AddDotnetEntry(IDbConnection connection, DotnetMetric metric)
        {
            connection.Execute(
                $"INSERT INTO {Values.DotnetMetricsTable}(agentId,time,value) VALUES (@agentId,@time,@value);",
                new
                {
                    agentId = metric.AgentId,
                    time = metric.Time,
                    value = metric.Value
                });
        }

        private static void AddHddEntry(IDbConnection connection, HddMetric metric)
        {
            connection.Execute(
                $"INSERT INTO {Values.HddMetricsTable}(agentId,time,value) VALUES (@agentId,@time,@value);",
                new
                {
                    agentId = metric.AgentId,
                    time = metric.Time,
                    value = metric.Value
                });
        }

        private static void AddNetworkEntry(IDbConnection connection, NetworkMetric metric)
        {
            connection.Execute(
                $"INSERT INTO {Values.NetworkMetricsTable}(agentId,time,value) VALUES (@agentId,@time,@value);",
                new
                {
                    agentId = metric.AgentId,
                    time = metric.Time,
                    value = metric.Value
                });
        }

        private static void AddRamEntry(IDbConnection connection, RamMetric metric)
        {
            connection.Execute(
                $"INSERT INTO {Values.RamMetricsTable}(agentId,time,value) VALUES (@agentId,@time,@value);",
                new
                {
                    agentId = metric.AgentId,
                    time = metric.Time,
                    value = metric.Value
                });
        }
    }
}