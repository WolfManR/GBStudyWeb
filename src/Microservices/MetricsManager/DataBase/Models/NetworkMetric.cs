using Common;

namespace MetricsManager.DataBase.Models
{
    public record NetworkMetric : MetricEntityBase
    {
        public int Value { get; init; }
    }
}