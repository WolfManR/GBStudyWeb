using System;
using System.Data;
using Bogus;
using Common.Configuration;
using Dapper;
using FluentMigrator.Runner;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent.DataBase
{
    // TODO: Remove commented code in next version
    public class SQLiteInitializer
    {
        private readonly SQLiteContainer _container;
        private readonly IMigrationRunner _migrationRunner;


        //private readonly Faker<CpuMetric> _cpuMetricsGenerator = new Faker<CpuMetric>().Rules((f, m) =>
        //{
        //    m.Value = f.Random.Int();
        //    m.Time = f.Date.RecentOffset(32).ToUnixTimeSeconds();
        //});

        //private readonly Faker<DotnetMetric> _dotnetMetricsGenerator = new Faker<DotnetMetric>().Rules((f, m) =>
        //{
        //    m.Value = f.Random.Int();
        //    m.Time = f.Date.RecentOffset(32).ToUnixTimeSeconds();
        //});

        //private readonly Faker<HddMetric> _hddMetricsGenerator = new Faker<HddMetric>().Rules((f, m) =>
        //{
        //    m.Value = f.Random.Int();
        //    m.Time = f.Date.RecentOffset(32).ToUnixTimeSeconds();
        //});

        //private readonly Faker<NetworkMetric> _networkMetricsGenerator = new Faker<NetworkMetric>().Rules((f, m) =>
        //{
        //    m.Value = f.Random.Int();
        //    m.Time = f.Date.RecentOffset(32).ToUnixTimeSeconds();
        //});

        //private readonly Faker<RamMetric> _ramMetricsGenerator = new Faker<RamMetric>().Rules((f, m) =>
        //{
        //    m.Value = f.Random.Int();
        //    m.Time = f.Date.RecentOffset(32).ToUnixTimeSeconds();
        //});


        public SQLiteInitializer(SQLiteContainer container, IMigrationRunner migrationRunner)
        {
            _container = container;
            _migrationRunner = migrationRunner;
        }


        public void Init()
        {

            _migrationRunner.MigrateUp();


            //using var connection = _container.CreateConnection();

            //// CPU
            //if (IsTableNotFilled(connection, Values.CpuMetricsTable))
            //    foreach (var cpu in _cpuMetricsGenerator.Generate(10))
            //        AddCpuEntry(connection, cpu);

            //// DOTNET
            //if (IsTableNotFilled(connection, Values.DotnetMetricsTable))
            //    foreach (var dotnet in _dotnetMetricsGenerator.Generate(10))
            //        AddDotnetEntry(connection, dotnet);

            //// HDD
            //if (IsTableNotFilled(connection, Values.HddMetricsTable))
            //    foreach (var hdd in _hddMetricsGenerator.Generate(10))
            //        AddHddEntry(connection, hdd);

            //// NETWORK
            //if (IsTableNotFilled(connection, Values.NetworkMetricsTable))
            //    foreach (var network in _networkMetricsGenerator.Generate(10))
            //        AddNetworkEntry(connection, network);

            ////RAM
            //if (IsTableNotFilled(connection, Values.RamMetricsTable))
            //    foreach (var ram in _ramMetricsGenerator.Generate(10))
            //        AddRamEntry(connection, ram);
        }

        //private static bool IsTableNotFilled(IDbConnection connection, string tableName)
        //{
        //    var count = connection.ExecuteScalar<int>($"SELECT Count(*) FROM {tableName};");
        //    return count <= 0;
        //}

        //private static void AddCpuEntry(IDbConnection connection, CpuMetric metric)
        //{
        //    connection.Execute(
        //        $"INSERT INTO {Values.CpuMetricsTable}(value,time) VALUES (@value,@time);",
        //        new
        //        {
        //            value = metric.Value,
        //            time = metric.Time
        //        });
        //}

        //private static void AddDotnetEntry(IDbConnection connection, DotnetMetric metric)
        //{
        //    connection.Execute(
        //        $"INSERT INTO {Values.DotnetMetricsTable}(value,time) VALUES (@value,@time);",
        //        new
        //        {
        //            value = metric.Value,
        //            time = metric.Time
        //        });
        //}

        //private static void AddHddEntry(IDbConnection connection, HddMetric metric)
        //{
        //    connection.Execute(
        //        $"INSERT INTO {Values.HddMetricsTable}(value,time) VALUES (@value,@time);",
        //        new
        //        {
        //            value = metric.Value,
        //            time = metric.Time
        //        });
        //}

        //private static void AddNetworkEntry(IDbConnection connection, NetworkMetric metric)
        //{
        //    connection.Execute(
        //        $"INSERT INTO {Values.NetworkMetricsTable}(value,time) VALUES (@value,@time);",
        //        new
        //        {
        //            value = metric.Value,
        //            time = metric.Time
        //        });
        //}

        //private static void AddRamEntry(IDbConnection connection, RamMetric metric)
        //{
        //    connection.Execute(
        //        $"INSERT INTO {Values.RamMetricsTable}(value,time) VALUES (@value,@time);",
        //        new
        //        {
        //            value = metric.Value,
        //            time = metric.Time
        //        });
        //}
    }
}