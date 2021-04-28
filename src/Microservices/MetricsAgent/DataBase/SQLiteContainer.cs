using System.Data.SQLite;

namespace MetricsAgent.DataBase
{
    public class SQLiteContainer
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public SQLiteConnection CreateConnection() => new SQLiteConnection(ConnectionString);
    }

    public class SQLiteInitializer
    {
        public void Init(SQLiteConnection connection)
        {
            RecreateTable(connection,"cpumetrics","id INTEGER PRIMARY KEY, value INT, time INT");
        }

        private void RecreateTable(SQLiteConnection connection, string tableName, string tableEntrySchema)
        {
            using var command = new SQLiteCommand(connection);
            command.CommandText = $"DROP TABLE IF EXIST {tableName}";
            command.ExecuteNonQuery();
            command.CommandText = $"CREATE TABLE {tableName}({tableEntrySchema})";
            command.ExecuteNonQuery();
        }
    }
}