using System.Threading.Tasks;
using MetricsManager.DataBase.Interfaces;
using Quartz;

namespace MetricsManager.Jobs.MetricsJobs
{
    [DisallowConcurrentExecution]
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _repository;

        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _repository.Create(new(){});
            return Task.CompletedTask;
        }
    }
}