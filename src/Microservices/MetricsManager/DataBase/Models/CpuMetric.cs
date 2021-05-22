using Common;

namespace MetricsManager.DataBase.Models
{
    public record CpuMetric : MetricEntityBase
    {
        public int Value { get; init; }
    }
}