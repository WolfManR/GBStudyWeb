using System.Data.SQLite;
using Bogus;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent.DataBase
{
    public class SQLiteInitializer
    {
        private readonly SQLiteContainer _container;
        
        
        private Faker<CpuMetric> cpuMetricsGenerator = new Faker<CpuMetric>().Rules((f, m) =>
        {
            m.Value = f.Random.Int();
            m.Time = f.Date.RecentOffset(32).ToUnixTimeSeconds();
        });
        private Faker<DotnetMetric> dotnetMetricsGenerator = new Faker<DotnetMetric>().Rules((f, m) =>
        {
            m.Value = f.Random.Int();
            m.Time = f.Date.RecentOffset(32).ToUnixTimeSeconds();
        });
        private Faker<HddMetric> hddMetricsGenerator = new Faker<HddMetric>().Rules((f, m) =>
        {
            m.Value = f.Random.Int();
            m.Time = f.Date.RecentOffset(32).ToUnixTimeSeconds();
        });
        private Faker<NetworkMetric> networkMetricsGenerator = new Faker<NetworkMetric>().Rules((f, m) =>
        {
            m.Value = f.Random.Int();
            m.Time = f.Date.RecentOffset(32).ToUnixTimeSeconds();
        });
        private Faker<RamMetric> ramMetricsGenerator = new Faker<RamMetric>().Rules((f, m) =>
        {
            m.Value = f.Random.Int();
            m.Time = f.Date.RecentOffset(32).ToUnixTimeSeconds();
        });

        
        public SQLiteInitializer(SQLiteContainer container)
        {
            _container = container;
        }

        
        public void Init()
        {
            using var connection = _container.CreateConnection();
            connection.Open();
            
            // CPU
            RecreateTable(connection,"cpumetrics","id INTEGER PRIMARY KEY, value INT, time INT");
            foreach (var cpu in cpuMetricsGenerator.Generate(10))
                AddCpuEntry(connection,cpu);
            // DOTNET
            RecreateTable(connection,"dotnetmetrics","id INTEGER PRIMARY KEY, value INT, time INT");
            foreach (var dotnet in dotnetMetricsGenerator.Generate(10))
                AddDotnetEntry(connection,dotnet);
            // HDD
            RecreateTable(connection,"hddmetrics","id INTEGER PRIMARY KEY, value INT, time INT");
            foreach (var hdd in hddMetricsGenerator.Generate(10))
                AddHddEntry(connection,hdd);
            // NETWORK
            RecreateTable(connection,"networkmetrics","id INTEGER PRIMARY KEY, value INT, time INT");
            foreach (var network in networkMetricsGenerator.Generate(10))
                AddNetworkEntry(connection,network);
            //RAM
            RecreateTable(connection,"rammetrics","id INTEGER PRIMARY KEY, value INT, time INT");
            foreach (var ram in ramMetricsGenerator.Generate(10))
                AddRamEntry(connection,ram);
            
            connection.Close();
        }

        
        private void RecreateTable(SQLiteConnection connection, string tableName, string tableEntrySchema)
        {
            using var command = new SQLiteCommand(connection);
            command.CommandText = $"DROP TABLE IF EXISTS {tableName}";
            command.ExecuteNonQuery();
            command.CommandText = $"CREATE TABLE {tableName}({tableEntrySchema})";
            command.ExecuteNonQuery();
        }
        
        
        private void AddCpuEntry(SQLiteConnection connection, CpuMetric metric)
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO cpumetrics(value,time) VALUES (@value,@time);";
            cmd.Parameters.AddWithValue("@value", metric.Value);
            cmd.Parameters.AddWithValue("@time", metric.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        
        private void AddDotnetEntry(SQLiteConnection connection, DotnetMetric metric)
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO dotnetmetrics(value,time) VALUES (@value,@time);";
            cmd.Parameters.AddWithValue("@value", metric.Value);
            cmd.Parameters.AddWithValue("@time", metric.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        
        private void AddHddEntry(SQLiteConnection connection, HddMetric metric)
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO hddmetrics(value,time) VALUES (@value,@time);";
            cmd.Parameters.AddWithValue("@value", metric.Value);
            cmd.Parameters.AddWithValue("@time", metric.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        
        private void AddNetworkEntry(SQLiteConnection connection, NetworkMetric metric)
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO networkmetrics(value,time) VALUES (@value,@time);";
            cmd.Parameters.AddWithValue("@value", metric.Value);
            cmd.Parameters.AddWithValue("@time", metric.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        
        private void AddRamEntry(SQLiteConnection connection, RamMetric metric)
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO rammetrics(value,time) VALUES (@value,@time);";
            cmd.Parameters.AddWithValue("@value", metric.Value);
            cmd.Parameters.AddWithValue("@time", metric.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}