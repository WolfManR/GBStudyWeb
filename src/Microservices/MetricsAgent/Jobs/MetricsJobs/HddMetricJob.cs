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
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _repository;
        private readonly ILogger<HddMetricJob> _logger;
        private readonly PerformanceCounter _counter;

        public HddMetricJob(IHddMetricsRepository repository, ILogger<HddMetricJob> logger)
        {
            _repository = repository;
            _logger = logger;
            _counter = new("LogicalDisk", "Free Megabytes", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var hddMetric = Convert.ToInt32(_counter.NextValue());
                var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                _repository.Create(new() { Time = time, Value = hddMetric });
            }
            catch (OverflowException e)
            {
                _logger.LogError("Cant create hdd metric, metric value overflow limit of integer", e);
            }
            catch (SQLiteException e) when (e.Message.Contains("no such table"))
            {
                _logger.LogDebug("Table for hdd metrics still not exist");
            }
            catch (Exception e)
            {
                _logger.LogError(LogEvents.EntityCreationFailure, "Cant save hdd metric", e);
            }
            return Task.CompletedTask;
        }
    }
}