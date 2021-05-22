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
    public class DotnetMetricJob : IJob
    {
        private readonly IDotnetMetricsRepository _repository;
        private readonly ILogger<DotnetMetricJob> _logger;
        private readonly PerformanceCounter _counter;

        public DotnetMetricJob(IDotnetMetricsRepository repository, ILogger<DotnetMetricJob> logger)
        {
            _repository = repository;
            _logger = logger;
            _counter = new(".NET CLR Exceptions", "# of Exceps Thrown", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var dotnetMetric = Convert.ToInt32(_counter.NextValue());
                var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                _repository.Create(new() { Time = time, Value = dotnetMetric });
            }
            catch (OverflowException e)
            {
                _logger.LogError("Cant create dotnet metric, metric value overflow limit of integer", e);
            }
            catch (SQLiteException e) when (e.Message.Contains("no such table"))
            {
                _logger.LogDebug("Table for dotnet metrics still not exist");
            }
            catch (Exception e)
            {
                _logger.LogError(LogEvents.EntityCreationFailure, "Cant save dotnet metric", e);
            }
            return Task.CompletedTask;
        }
    }
}