namespace MetricsAgent.DataBase.Models
{
    public class NetworkMetric : IEntity<int>
    {
        public int Id { get; init; }
        public long Time { get; set; }
        public int Value { get; set; }
    }
}