using System.Threading.Tasks;
using Quartz;

namespace MetricsAgent.Jobs.MetricsJobs
{
    public class CpuMetricJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return null;
        }
    }
}