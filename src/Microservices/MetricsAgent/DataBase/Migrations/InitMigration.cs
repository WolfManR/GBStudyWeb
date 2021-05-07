using Common.Configuration;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace MetricsAgent.DataBase.Migrations
{
    [Migration(1)]
    public class InitMigration : Migration
    {
        public override void Up()
        {
            static void CreateSchema(ICreateTableWithColumnSyntax tableSchema)
            {
                tableSchema.WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("Value").AsInt32()
                    .WithColumn("Time").AsInt64();
            }

            CreateSchema(Create.Table(Values.CpuMetricsTable));

            CreateSchema(Create.Table(Values.DotnetMetricsTable));

            CreateSchema(Create.Table(Values.HddMetricsTable));

            CreateSchema(Create.Table(Values.NetworkMetricsTable));

            CreateSchema(Create.Table(Values.RamMetricsTable));
        }

        public override void Down()
        {
            Delete.Table(Values.CpuMetricsTable);
            Delete.Table(Values.DotnetMetricsTable);
            Delete.Table(Values.HddMetricsTable);
            Delete.Table(Values.NetworkMetricsTable);
            Delete.Table(Values.RamMetricsTable);
        }
    }
}