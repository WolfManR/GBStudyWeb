namespace MetricsManager.DataBase.Models
{
    public record HddMetric : MetricEntityBase<int>
    {
        public int Value { get; init; }
    }
}