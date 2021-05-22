using System.Data.SQLite;

namespace MetricsManager.DataBase
{
    public class SQLiteContainer
    {
        private readonly string _connectionString;
        
        public SQLiteContainer(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public SQLiteConnection CreateConnection() => new(_connectionString);
    }
}