using Common;

namespace MetricsManager.DataBase.Models
{
    public record RamMetric : MetricEntityBase
    {
        public int Value { get; init; }
    }
}