using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.DataBase.Interfaces;
using MetricsManager.DataBase.Models;
using MetricsManager.Services.Client;
using MetricsManager.Services.Client.Requests;
using MetricsManager.Services.Client.Responses;
using Quartz;

namespace MetricsManager.Jobs.MetricsJobs
{
    [DisallowConcurrentExecution]
    public class RamMetricJob : MetricJob<RamMetric, int>
    {
        private readonly IMetricsClient _client;

        public RamMetricJob(IRamMetricsRepository metricsRepository, IMetricsClient client, IAgentsRepository agentsRepository) :
            base(metricsRepository,agentsRepository)
        {
            _client = client;
        }

        protected override async Task<IEnumerable<RamMetric>> GetMetricsByTimePeriod(AgentInfo agent, DateTimeOffset @from, DateTimeOffset to)
        {
            var response = await _client.GetMetrics(PrepareRequest(agent.Uri, from, to)).ConfigureAwait(false);

            var metrics = response.Select(r => ToMetric(agent.Id, r)).ToArray();
            return metrics;
        }

        private static RamMetricsRequest PrepareRequest(string agentUrl, DateTimeOffset from, DateTimeOffset to)
        {
            return new()
            {
                AgentUrl = agentUrl,
                FromTime = from,
                ToTime = to
            };
        }

        private static RamMetric ToMetric(int agentId, RamMetricResponse response)
        {
            return new()
            {
                AgentId = agentId,
                Time = response.Time.ToUnixTimeSeconds(),
                Value = response.Value
            };
        }
    }
}