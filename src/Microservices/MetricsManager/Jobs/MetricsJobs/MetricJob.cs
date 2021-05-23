using MetricsManager.DataBase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Immutable;
using Quartz;
using MetricsManager.DataBase.Interfaces;

namespace MetricsManager.Jobs.MetricsJobs
{
    public abstract class MetricJob<TMetric, TIdentifier> : IJob where TMetric : IMetricEntity<TIdentifier, int>
    {
        private readonly IRepository<TMetric, TIdentifier, int> _metricsRepository;
        private readonly IAgentsRepository _agentsRepository;

        protected MetricJob(IRepository<TMetric, TIdentifier, int> metricsRepository, IAgentsRepository agentsRepository)
        {
            _metricsRepository = metricsRepository;
            _agentsRepository = agentsRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var agents = _agentsRepository.Get();
            var metrics = await Task.WhenAll(agents.Select(GetAgentMetrics));
            AddNewMetrics(metrics.SelectMany(agentMetrics => agentMetrics).ToImmutableArray());
        }

        private async Task<IEnumerable<TMetric>> GetAgentMetrics(AgentInfo agent)
        {
            if (!agent.IsEnabled) return Enumerable.Empty<TMetric>();
            return await GetMetricsByTimePeriod(agent, GetLastMetricDate(agent.Id), DateTimeOffset.UtcNow).ConfigureAwait(false);
        }
        
        private DateTimeOffset GetLastMetricDate(int agentId)
        {
            return _metricsRepository.GetAgentLastMetricDate(agentId);
        }

        private void AddNewMetrics(ImmutableArray<TMetric> metrics)
        {
            for (var i = 0; i < metrics.Length; i++)
            {
                _metricsRepository.Create(metrics[i]);
            }
        }

        protected abstract Task<IEnumerable<TMetric>> GetMetricsByTimePeriod(AgentInfo agent, DateTimeOffset from, DateTimeOffset to);
    }
}