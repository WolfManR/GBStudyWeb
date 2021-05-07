using System;
using System.Data;
using Bogus;
using Common.Configuration;
using Dapper;
using FluentMigrator.Runner;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent.DataBase
{
    public class SQLiteInitializer
    {
        private readonly SQLiteContainer _container;
        private readonly IMigrationRunner _migrationRunner;


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


        public SQLiteInitializer(SQLiteContainer container, IMigrationRunner migrationRunner)
        {
            _container = container;
            _migrationRunner = migrationRunner;
        }


        public void Init()
        {
            _migrationRunner.MigrateUp();


            using var connection = _container.CreateConnection();

            // CPU
            if (IsTableNotFilled(connection, Values.CpuMetricsTable))
                foreach (var cpu in cpuMetricsGenerator.Generate(10))
                    AddCpuEntry(connection, cpu);

            // DOTNET
            if (IsTableNotFilled(connection, Values.DotnetMetricsTable))
                foreach (var dotnet in dotnetMetricsGenerator.Generate(10))
                    AddDotnetEntry(connection, dotnet);

            // HDD
            if (IsTableNotFilled(connection, Values.HddMetricsTable))
                foreach (var hdd in hddMetricsGenerator.Generate(10))
                    AddHddEntry(connection, hdd);

            // NETWORK
            if (IsTableNotFilled(connection, Values.NetworkMetricsTable))
                foreach (var network in networkMetricsGenerator.Generate(10))
                    AddNetworkEntry(connection, network);

            //RAM
            if (IsTableNotFilled(connection, Values.RamMetricsTable))
                foreach (var ram in ramMetricsGenerator.Generate(10))
                    AddRamEntry(connection, ram);
        }

        private bool IsTableNotFilled(IDbConnection connection, string tableName)
        {
            var count = connection.ExecuteScalar<int>($"SELECT Count(*) FROM {tableName};");
            return count <= 0;
        }

        private void AddCpuEntry(IDbConnection connection, CpuMetric metric)
        {
            connection.Execute(
                $"INSERT INTO {Values.CpuMetricsTable}(value,time) VALUES (@value,@time);",
                new
                {
                    value = metric.Value,
                    time = metric.Time
                });
        }

        private void AddDotnetEntry(IDbConnection connection, DotnetMetric metric)
        {
            connection.Execute(
                $"INSERT INTO {Values.DotnetMetricsTable}(value,time) VALUES (@value,@time);",
                new
                {
                    value = metric.Value,
                    time = metric.Time
                });
        }

        private void AddHddEntry(IDbConnection connection, HddMetric metric)
        {
            connection.Execute(
                $"INSERT INTO {Values.HddMetricsTable}(value,time) VALUES (@value,@time);",
                new
                {
                    value = metric.Value,
                    time = metric.Time
                });
        }

        private void AddNetworkEntry(IDbConnection connection, NetworkMetric metric)
        {
            connection.Execute(
                $"INSERT INTO {Values.NetworkMetricsTable}(value,time) VALUES (@value,@time);",
                new
                {
                    value = metric.Value,
                    time = metric.Time
                });
        }

        private void AddRamEntry(IDbConnection connection, RamMetric metric)
        {
            connection.Execute(
                $"INSERT INTO {Values.RamMetricsTable}(value,time) VALUES (@value,@time);",
                new
                {
                    value = metric.Value,
                    time = metric.Time
                });
        }
    }
}