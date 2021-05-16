using System.Threading.Tasks;
using MetricsManager.DataBase.Interfaces;
using Quartz;

namespace MetricsManager.Jobs.MetricsJobs
{
    [DisallowConcurrentExecution]
    public class DotnetMetricJob : IJob
    {
        private readonly IDotnetMetricsRepository _repository;

        public DotnetMetricJob(IDotnetMetricsRepository repository)
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