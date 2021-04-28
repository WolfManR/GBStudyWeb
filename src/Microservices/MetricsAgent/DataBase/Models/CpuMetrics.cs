using System;

namespace MetricsAgent.DataBase.Models
{
    public class CpuMetrics : IEntity<int>
    {
        public int Scu { get; init; }
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}