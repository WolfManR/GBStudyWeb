using System.Data.SQLite;
using Bogus;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent.DataBase
{
    public class SQLiteContainer
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public SQLiteConnection CreateConnection() => new SQLiteConnection(ConnectionString);
    }

    public class SQLiteInitializer
    {
        private readonly SQLiteContainer _container;

        public SQLiteInitializer(SQLiteContainer container)
        {
            _container = container;
        }

        private Faker<CpuMetrics> cpuMetricsGenerator = new Faker<CpuMetrics>().Rules((f, m) =>
        {
            m.Value = f.Random.Int();
            m.Time = f.Date.RecentOffset(32);
        });
        
        public void Init()
        {
            using var connection = _container.CreateConnection();
            connection.Open();
            RecreateTable(connection,"cpumetrics","id INTEGER PRIMARY KEY, value INT, time INT");
            foreach (var cpu in cpuMetricsGenerator.Generate(10))
                AddCpuEntry(connection,cpu);
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
        
        private void AddCpuEntry(SQLiteConnection connection, CpuMetrics metric)
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO cpumetrics(value,time) VALUES (@value,@time);";
            cmd.Parameters.AddWithValue("@value", metric.Value);
            cmd.Parameters.AddWithValue("@time", metric.Time.ToUnixTimeSeconds());
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}