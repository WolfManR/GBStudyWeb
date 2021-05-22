using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.Threading.Tasks;
using Common;
using MetricsAgent.DataBase.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace MetricsAgent.Jobs.MetricsJobs
{
    [DisallowConcurrentExecution]
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricJob> _logger;
        private readonly PerformanceCounter _counter;

        public CpuMetricJob(ICpuMetricsRepository repository, ILogger<CpuMetricJob> logger)
        {
            _repository = repository;
            _logger = logger;
            _counter = new("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuMetric = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            try
            {
                _repository.Create(new() {Time = time, Value = cpuMetric});
            }
            catch (SQLiteException e) when (e.Message.Contains("no such table"))
            {
                _logger.LogDebug("Table for cpu metrics still not exist");
            }
            catch (Exception e)
            {
                _logger.LogError(LogEvents.EntityCreationFailure, "Cant save cpu metric", e);
            }
            
            return Task.CompletedTask;
        }
    }
}