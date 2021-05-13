using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DataBase.Interfaces;
using Quartz;

namespace MetricsAgent.Jobs.MetricsJobs
{
    [DisallowConcurrentExecution]
    public class DotnetMetricJob : IJob
    {
        private readonly IDotnetMetricsRepository _repository;
        private readonly PerformanceCounter _counter;

        public DotnetMetricJob(IDotnetMetricsRepository repository)
        {
            _repository = repository;
            _counter = new(".NET CLR Exceptions", "# of Exceps Thrown", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var dotnetMetric = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new() { Time = time, Value = dotnetMetric });
            return Task.CompletedTask;
        }
    }
}