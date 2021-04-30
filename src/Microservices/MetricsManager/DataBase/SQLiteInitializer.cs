using System.Data.SQLite;

namespace MetricsManager.DataBase
{
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