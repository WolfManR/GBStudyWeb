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
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _metricsRepository;
        private readonly IMetricsClient _client;
        private readonly IAgentsRepository _agentsRepository;

        public NetworkMetricJob(INetworkMetricsRepository metricsRepository, IMetricsClient client, IAgentsRepository agentsRepository)
        {
            _metricsRepository = metricsRepository;
            _client = client;
            _agentsRepository = agentsRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var agents = _agentsRepository.Get();
            for (var i = 0; i < agents.Count; i++)
            {
                var agent = agents[i];
                if (!agent.IsEnabled) continue;

                var response = await GetMetricsByTimePeriod(agent.Uri, GetLastMetricDate(agent.Id), DateTimeOffset.UtcNow);
                var metrics = response.Select(r => new NetworkMetric()
                {
                    AgentId = agent.Id,
                    Time = r.Time.ToUnixTimeSeconds(),
                    Value = r.Value
                }).ToArray();
                AddNewMetrics(metrics);
            }
        }

        private async Task<IEnumerable<NetworkMetricResponse>> GetMetricsByTimePeriod(string agentUrl, DateTimeOffset from, DateTimeOffset to)
        {
            var request = new NetworkMetricsRequest()
            {
                AgentUrl = agentUrl,
                FromTime = from,
                ToTime = to
            };
            return await _client.GetMetrics(request).ConfigureAwait(false);
        }

        private DateTimeOffset GetLastMetricDate(int agentId)
        {
            return _metricsRepository.GetAgentLastMetricDate(agentId);
        }

        private void AddNewMetrics(NetworkMetric[] metrics)
        {
            for (var i = 0; i < metrics.Length; i++)
            {
                _metricsRepository.Create(metrics[i]);
            }
        }
    }
}