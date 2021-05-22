namespace MetricsManager.DataBase.Models
{
    public record DotnetMetric : MetricEntityBase<int>
    {
        public int Value { get; init; }
    }
}