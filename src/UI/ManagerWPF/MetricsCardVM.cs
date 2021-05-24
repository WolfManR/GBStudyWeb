using System;
using LiveCharts.Wpf;
using LiveCharts;

namespace ManagerWPF
{
    public class MetricsCardVM
    {
        public MetricsCardVM()
        {
            ColumnSeriesValues = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = _metrics
                }
            };
        }

        public DateTimeOffset Last { get; set; } = new DateTimeOffset();
        private ChartValues<double> _metrics = new ChartValues<double>();
        public SeriesCollection ColumnSeriesValues { get; set; }
        
        public void AddMetric(double value)
        {
            _metrics.Add(value);
        }
    }
}