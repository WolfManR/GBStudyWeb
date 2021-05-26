using Common.Configuration;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace MetricsManager.DataBase.Migrations
{
    [Migration(1)]
    public class InitMigration : Migration
    {
        public override void Up()
        {
            static void CreateSchema(ICreateTableWithColumnSyntax tableSchema)
            {
                tableSchema.WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("AgentId").AsInt64()
                    .WithColumn("Time").AsInt64()
                    .WithColumn("Value").AsInt32();
            }

            Create.Table(Values.AgentsMetricsTable)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Uri").AsString(Values.LongestUrlLength)
                .WithColumn("IsEnabled").AsBoolean().NotNullable();

            CreateSchema(Create.Table(Values.CpuMetricsTable));

            CreateSchema(Create.Table(Values.DotnetMetricsTable));

            CreateSchema(Create.Table(Values.HddMetricsTable));

            CreateSchema(Create.Table(Values.NetworkMetricsTable));

            CreateSchema(Create.Table(Values.RamMetricsTable));
        }

        public override void Down()
        {
            Delete.Table(Values.AgentsMetricsTable);
            Delete.Table(Values.CpuMetricsTable);
            Delete.Table(Values.DotnetMetricsTable);
            Delete.Table(Values.HddMetricsTable);
            Delete.Table(Values.NetworkMetricsTable);
            Delete.Table(Values.RamMetricsTable);
        }
    }
}