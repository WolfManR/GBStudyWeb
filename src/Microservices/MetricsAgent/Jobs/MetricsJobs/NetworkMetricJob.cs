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
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly ILogger<NetworkMetricJob> _logger;
        private readonly PerformanceCounter _counter;

        public NetworkMetricJob(INetworkMetricsRepository repository, ILogger<NetworkMetricJob> logger)
        {
            _repository = repository;
            _logger = logger;
            _counter = new("IPsec Connections", "Total Number current Connections");
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var networkMetric = Convert.ToInt32(_counter.NextValue());
                var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                _repository.Create(new() { Time = time, Value = networkMetric });
            }
            catch (OverflowException e)
            {
                _logger.LogError("Cant create network metric, metric value overflow limit of integer", e);
            }
            catch (SQLiteException e) when (e.Message.Contains("no such table"))
            {
                _logger.LogDebug("Table for network metrics still not exist");
            }
            catch (Exception e)
            {
                _logger.LogError(LogEvents.EntityCreationFailure, "Cant save network metric", e);
            }
            return Task.CompletedTask;
        }
    }
}