using System;
using System.Threading.Tasks;
using MetricsManager.DataBase.Interfaces;
using Quartz;

namespace MetricsManager.Jobs.MetricsJobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _repository;

        public NetworkMetricJob(INetworkMetricsRepository repository)
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