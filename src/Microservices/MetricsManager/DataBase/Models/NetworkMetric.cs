namespace MetricsManager.DataBase.Models
{
    public record NetworkMetric : MetricEntityBase<int>
    {
        public int Value { get; init; }
    }
}