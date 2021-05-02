using System.Data;
using System.Data.SQLite;
using Bogus;
using Dapper;
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
            RecreateTable(connection, "cpumetrics", "id INTEGER PRIMARY KEY, value INT, time INT");
            foreach (var cpu in cpuMetricsGenerator.Generate(10))
                AddCpuEntry(connection, cpu);
            // DOTNET
            RecreateTable(connection, "dotnetmetrics", "id INTEGER PRIMARY KEY, value INT, time INT");
            foreach (var dotnet in dotnetMetricsGenerator.Generate(10))
                AddDotnetEntry(connection, dotnet);
            // HDD
            RecreateTable(connection, "hddmetrics", "id INTEGER PRIMARY KEY, value INT, time INT");
            foreach (var hdd in hddMetricsGenerator.Generate(10))
                AddHddEntry(connection, hdd);
            // NETWORK
            RecreateTable(connection, "networkmetrics", "id INTEGER PRIMARY KEY, value INT, time INT");
            foreach (var network in networkMetricsGenerator.Generate(10))
                AddNetworkEntry(connection, network);
            //RAM
            RecreateTable(connection, "rammetrics", "id INTEGER PRIMARY KEY, value INT, time INT");
            foreach (var ram in ramMetricsGenerator.Generate(10))
                AddRamEntry(connection, ram);

            connection.Close();
        }


        private void RecreateTable(IDbConnection connection, string tableName, string tableEntrySchema)
        {
            connection.Execute($"DROP TABLE IF EXISTS {tableName}");
            connection.Execute($"CREATE TABLE {tableName}({tableEntrySchema})");
        }


        private void AddCpuEntry(SQLiteConnection connection, CpuMetric metric)
        {
            connection.Execute(
                "INSERT INTO cpumetrics(value,time) VALUES (@value,@time);",
                new
                {
                    value = metric.Value,
                    time = metric.Time
                });
        }

        private void AddDotnetEntry(IDbConnection connection, DotnetMetric metric)
        {
            connection.Execute(
                "INSERT INTO dotnetmetrics(value,time) VALUES (@value,@time);",
                new
                {
                    value = metric.Value,
                    time = metric.Time
                });
        }

        private void AddHddEntry(IDbConnection connection, HddMetric metric)
        {
            connection.Execute(
                "INSERT INTO hddmetrics(value,time) VALUES (@value,@time);",
                new
                {
                    value = metric.Value,
                    time = metric.Time
                });
        }

        private void AddNetworkEntry(IDbConnection connection, NetworkMetric metric)
        {
            connection.Execute(
                "INSERT INTO networkmetrics(value,time) VALUES (@value,@time);",
                new
                {
                    value = metric.Value,
                    time = metric.Time
                });
        }

        private void AddRamEntry(IDbConnection connection, RamMetric metric)
        {
            connection.Execute(
                "INSERT INTO rammetrics(value,time) VALUES (@value,@time);",
                new
                {
                    value = metric.Value,
                    time = metric.Time
                });
        }
    }
}