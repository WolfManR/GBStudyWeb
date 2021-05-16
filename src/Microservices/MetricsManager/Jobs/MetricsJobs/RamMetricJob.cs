using System;
using System.Threading.Tasks;
using MetricsManager.DataBase.Interfaces;
using Quartz;

namespace MetricsManager.Jobs.MetricsJobs
{
    [DisallowConcurrentExecution]
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _repository;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _repository.Create(new() { });
            return Task.CompletedTask;
        }
    }
}