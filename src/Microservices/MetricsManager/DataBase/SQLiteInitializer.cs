using System.Data.SQLite;
using Bogus;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase
{
    public class SQLiteInitializer
    {
        private readonly SQLiteContainer _container;
        private const int AgentsCount = 4;
        
        private Faker<AgentInfo> agentsGenerator = new Faker<AgentInfo>().CustomInstantiator(f => new AgentInfo()
        {
            Id = f.IndexFaker,
            Uri = f.Internet.Url(),
            IsEnabled = f.Random.Bool()
        });
        
        private Faker<CpuMetric> cpuMetricsGenerator = new Faker<CpuMetric>().CustomInstantiator(f => new CpuMetric()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });
        
        private Faker<DotnetMetric> dotnetMetricsGenerator = new Faker<DotnetMetric>().CustomInstantiator(f => new DotnetMetric()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });
        private Faker<HddMetric> hddMetricsGenerator = new Faker<HddMetric>().CustomInstantiator(f => new HddMetric()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });
        private Faker<NetworkMetric> networkMetricsGenerator = new Faker<NetworkMetric>().CustomInstantiator(f => new NetworkMetric()
        {
            AgentId = f.Random.Int(1, AgentsCount),
            Value = f.Random.Int(),
            Time = f.Date.RecentOffset(32).ToUnixTimeSeconds(),
        });
        private Faker<RamMetric> ramMetricsGenerator = new Faker<RamMetric>().CustomInstantiator(f => new RamMetric()
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
            connection.Open();
            using var command = new SQLiteCommand(connection);
            
            // AGENTS
            RecreateTable(command,"agents","id INTEGER PRIMARY KEY, uri text, isenabled INT NOT NULL");
            foreach (var agent in agentsGenerator.Generate(AgentsCount))
                AddAgent(command,agent);
            // CPU
            RecreateTable(command,"cpumetrics","id INTEGER PRIMARY KEY, agentId INT NOT NULL, value INT, time INT");
            foreach (var cpu in cpuMetricsGenerator.Generate(10))
                AddCpuEntry(command,cpu);
            // DOTNET
            RecreateTable(command,"dotnetmetrics","id INTEGER PRIMARY KEY, agentId INT NOT NULL, value INT, time INT");
            foreach (var dotnet in dotnetMetricsGenerator.Generate(10))
                AddDotnetEntry(command,dotnet);
            // HDD
            RecreateTable(command,"hddmetrics","id INTEGER PRIMARY KEY, agentId INT NOT NULL, value INT, time INT");
            foreach (var hdd in hddMetricsGenerator.Generate(10))
                AddHddEntry(command,hdd);
            // NETWORK
            RecreateTable(command,"networkmetrics","id INTEGER PRIMARY KEY, agentId INT NOT NULL, value INT, time INT");
            foreach (var network in networkMetricsGenerator.Generate(10))
                AddNetworkEntry(command,network);
            //RAM
            RecreateTable(command,"rammetrics","id INTEGER PRIMARY KEY, agentId INT NOT NULL, value INT, time INT");
            foreach (var ram in ramMetricsGenerator.Generate(10))
                AddRamEntry(command,ram);
            
            connection.Close();
        }

        
        private void RecreateTable(SQLiteCommand command, string tableName, string tableEntrySchema)
        {
            command.Reset();
            command.CommandText = $"DROP TABLE IF EXISTS {tableName}";
            command.ExecuteNonQuery();
            command.CommandText = $"CREATE TABLE {tableName}({tableEntrySchema})";
            command.ExecuteNonQuery();
        }
        
        private void AddAgent(SQLiteCommand command, AgentInfo agent)
        {
            command.Reset();
            command.CommandText = "INSERT INTO agents(uri,isenabled) VALUES (@uri,@isenabled);";
            command.Parameters.AddWithValue("@uri", agent.Uri);
            command.Parameters.AddWithValue("@isenabled", agent.IsEnabled);
            command.ExecuteNonQuery();
        }
        
        private void AddCpuEntry(SQLiteCommand command, CpuMetric metric)
        {
            command.Reset();
            command.CommandText = "INSERT INTO cpumetrics(agentId,time,value) VALUES (@agentId,@time,@value);";
            command.Parameters.AddWithValue("@value", metric.Value);
            command.Parameters.AddWithValue("@time", metric.Time);
            command.Parameters.AddWithValue("@agentId", metric.AgentId);
            command.ExecuteNonQuery();
        }
        private void AddDotnetEntry(SQLiteCommand command, DotnetMetric metric)
        {
            command.Reset();
            command.CommandText = "INSERT INTO dotnetmetrics(agentId,time,value) VALUES (@agentId,@time,@value);";
            command.Parameters.AddWithValue("@value", metric.Value);
            command.Parameters.AddWithValue("@time", metric.Time);
            command.Parameters.AddWithValue("@agentId", metric.AgentId);
            command.ExecuteNonQuery();
        }
        private void AddHddEntry(SQLiteCommand command, HddMetric metric)
        {
            command.Reset();
            command.CommandText = "INSERT INTO hddmetrics(agentId,time,value) VALUES (@agentId,@time,@value);";
            command.Parameters.AddWithValue("@value", metric.Value);
            command.Parameters.AddWithValue("@time", metric.Time);
            command.Parameters.AddWithValue("@agentId", metric.AgentId);
            command.ExecuteNonQuery();
        }
        private void AddNetworkEntry(SQLiteCommand command, NetworkMetric metric)
        {
            command.Reset();
            command.CommandText = "INSERT INTO networkmetrics(agentId,time,value) VALUES (@agentId,@time,@value);";
            command.Parameters.AddWithValue("@value", metric.Value);
            command.Parameters.AddWithValue("@time", metric.Time);
            command.Parameters.AddWithValue("@agentId", metric.AgentId);
            command.ExecuteNonQuery();
        }
        private void AddRamEntry(SQLiteCommand command, RamMetric metric)
        {
            command.Reset();
            command.CommandText = "INSERT INTO rammetrics(agentId,time,value) VALUES (@agentId,@time,@value);";
            command.Parameters.AddWithValue("@value", metric.Value);
            command.Parameters.AddWithValue("@time", metric.Time);
            command.Parameters.AddWithValue("@agentId", metric.AgentId);
            command.ExecuteNonQuery();
        }
    }
}