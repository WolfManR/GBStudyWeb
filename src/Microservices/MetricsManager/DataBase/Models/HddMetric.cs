using Common;

namespace MetricsManager.DataBase.Models
{
    public record HddMetric : MetricEntityBase
    {
        public int Value { get; init; }
    }
}