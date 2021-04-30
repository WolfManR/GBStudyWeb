namespace MetricsAgent.DataBase.Models
{
    public class RamMetric : IEntity<int>
    {
        public int Id { get; init; }
        public long Time { get; set; }
        public int Value { get; set; }
    }
}