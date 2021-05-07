using System.Data.SQLite;

namespace MetricsManager.DataBase
{
    public class SQLiteContainer
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public SQLiteConnection CreateConnection() => new(ConnectionString);
    }
}