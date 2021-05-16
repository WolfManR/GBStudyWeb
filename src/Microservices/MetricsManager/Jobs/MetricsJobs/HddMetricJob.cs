using System;
using System.Threading.Tasks;
using MetricsManager.DataBase.Interfaces;
using Quartz;

namespace MetricsManager.Jobs.MetricsJobs
{
    [DisallowConcurrentExecution]
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _repository;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _repository.Create(new() {});
            return Task.CompletedTask;
        }
    }
}