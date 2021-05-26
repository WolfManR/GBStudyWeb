namespace MetricsManager.DataBase.Models
{
    public record RamMetric : MetricEntityBase<int>
    {
        public int Value { get; init; }
    }
}