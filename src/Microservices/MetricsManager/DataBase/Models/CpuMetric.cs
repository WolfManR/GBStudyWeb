namespace MetricsManager.DataBase.Models
{
    public record CpuMetric : MetricEntityBase<int>
    {
        public int Value { get; init; }
    }
}