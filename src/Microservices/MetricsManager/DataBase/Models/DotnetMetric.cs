using Common;

namespace MetricsManager.DataBase.Models
{
    public record DotnetMetric : MetricEntityBase
    {
        public int Value { get; init; }
    }
}