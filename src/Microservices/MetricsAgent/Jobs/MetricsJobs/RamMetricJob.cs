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
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _repository;
        private readonly ILogger<RamMetricJob> _logger;
        private readonly PerformanceCounter _counter;

        public RamMetricJob(IRamMetricsRepository repository, ILogger<RamMetricJob> logger)
        {
            _repository = repository;
            _logger = logger;
            _counter = new("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var ramMetric = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            try
            {
                _repository.Create(new()
                {
                    Time = time,
                    Value = ramMetric
                });
            }
            catch (SQLiteException e) when (e.Message.Contains("no such table"))
            {
                _logger.LogDebug("Table for ram metrics still not exist");
            }
            catch (Exception e)
            {
                _logger.LogError(LogEvents.EntityCreationFailure, "Cant save ram metric", e);
            }
            return Task.CompletedTask;
        }
    }
}