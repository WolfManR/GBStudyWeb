using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DataBase.Interfaces;
using Quartz;

namespace MetricsAgent.Jobs.MetricsJobs
{
    [DisallowConcurrentExecution]
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _repository;
        private readonly PerformanceCounter _counter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _counter = new("LogicalDisk", "Free Megabytes", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var hddMetric = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new() { Time = time, Value = hddMetric });
            return Task.CompletedTask;
        }
    }
}