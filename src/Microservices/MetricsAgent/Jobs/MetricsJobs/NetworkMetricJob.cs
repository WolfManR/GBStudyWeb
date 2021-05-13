using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DataBase.Interfaces;
using Quartz;

namespace MetricsAgent.Jobs.MetricsJobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly PerformanceCounter _counter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _counter = new("IPsec Connections", "Total Number current Connections");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var networkMetric = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new() { Time = time, Value = networkMetric });
            return Task.CompletedTask;
        }
    }
}