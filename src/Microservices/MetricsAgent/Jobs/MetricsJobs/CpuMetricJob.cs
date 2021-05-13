using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DataBase.Interfaces;
using Quartz;

namespace MetricsAgent.Jobs.MetricsJobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly PerformanceCounter _counter;

        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _counter = new("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuMetric = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new(){Time = time, Value = cpuMetric });
            return Task.CompletedTask;
        }
    }
}