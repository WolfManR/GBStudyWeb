using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DataBase.Interfaces;
using Quartz;

namespace MetricsAgent.Jobs.MetricsJobs
{
    [DisallowConcurrentExecution]
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _repository;
        private readonly PerformanceCounter _counter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _counter = new("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var ramMetric = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new() { Time = time, Value = ramMetric });
            return Task.CompletedTask;
        }
    }
}