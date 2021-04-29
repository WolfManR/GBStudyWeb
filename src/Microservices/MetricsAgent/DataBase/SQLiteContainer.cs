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
        private readonly SQLiteContainer _container;

        public SQLiteInitializer(SQLiteContainer container)
        {
            _container = container;
        }
        
        
        public void Init()
        {
            using var connection = _container.CreateConnection();
            connection.Open();
            RecreateTable(connection,"cpumetrics","id INTEGER PRIMARY KEY, value INT, time INT");
                
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
    }
}