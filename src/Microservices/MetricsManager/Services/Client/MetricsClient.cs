using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MetricsManager.Services.Client.Requests;
using MetricsManager.Services.SwaggerClient;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Services.Client
{
    public class MetricsClient : IMetricsClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<MetricsClient> _logger;

        public MetricsClient(HttpClient client, ILogger<MetricsClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<IEnumerable<CpuMetricResponse>> GetMetrics(CpuMetricsRequest request)
        {
            var apiClient = CreateClient(request.AgentUrl);
            try
            {
                var response = await apiClient.ApiMetricsCpuFromToAsync(request.FromTime, request.ToTime).ConfigureAwait(false);
                return response.Metrics;
            }
            catch (Exception e)
            {
                _logger.LogWarning(
                    e,
                    "Fail to connect to agent {url} to take CPU metrics in time range [{from} .. {to}]",
                    request.AgentUrl,
                    request.FromTime,
                    request.ToTime);
                return Enumerable.Empty<CpuMetricResponse>();
            }
        }

        public async Task<IEnumerable<DotnetMetricResponse>> GetMetrics(DotnetMetricsRequest request)
        {
            var apiClient = CreateClient(request.AgentUrl);
            try
            {
                var response = await apiClient.ApiMetricsDotnetFromToAsync(request.FromTime, request.ToTime).ConfigureAwait(false);
                return response.Metrics;
            }
            catch (Exception e)
            {
                _logger.LogWarning(
                    e,
                    "Fail to connect to agent {url} to take DOTNET metrics in time range [{from} .. {to}]",
                    request.AgentUrl,
                    request.FromTime,
                    request.ToTime);
                return Enumerable.Empty<DotnetMetricResponse>();
            }
        }

        public async Task<IEnumerable<HddMetricResponse>> GetMetrics(HddMetricsRequest request)
        {
            var apiClient = CreateClient(request.AgentUrl);
            try
            {
                var response = await apiClient.ApiMetricsHddFromToAsync(request.FromTime, request.ToTime).ConfigureAwait(false);
                return response.Metrics;
            }
            catch (Exception e)
            {
                _logger.LogWarning(
                    e,
                    "Fail to connect to agent {url} to take HDD metrics in time range [{from} .. {to}]",
                    request.AgentUrl,
                    request.FromTime,
                    request.ToTime);
                return Enumerable.Empty<HddMetricResponse>();
            }
        }

        public async Task<IEnumerable<NetworkMetricResponse>> GetMetrics(NetworkMetricsRequest request)
        {
            var apiClient = CreateClient(request.AgentUrl);
            try
            {
                var response = await apiClient.ApiMetricsNetworkFromToAsync(request.FromTime, request.ToTime).ConfigureAwait(false);
                return response.Metrics;
            }
            catch (Exception e)
            {
                _logger.LogWarning(
                    e,
                    "Fail to connect to agent {url} to take NETWORK metrics in time range [{from} .. {to}]",
                    request.AgentUrl,
                    request.FromTime,
                    request.ToTime);
                return Enumerable.Empty<NetworkMetricResponse>();
            }
        }

        public async Task<IEnumerable<RamMetricResponse>> GetMetrics(RamMetricsRequest request)
        {
            var apiClient = CreateClient(request.AgentUrl);
            try
            {
                var response = await apiClient.ApiMetricsRamFromToAsync(request.FromTime, request.ToTime).ConfigureAwait(false);
                return response.Metrics;
            }
            catch (Exception e)
            {
                _logger.LogWarning(
                    e,
                    "Fail to connect to agent {url} to take RAM metrics in time range [{from} .. {to}]",
                    request.AgentUrl,
                    request.FromTime,
                    request.ToTime);
                return Enumerable.Empty<RamMetricResponse>();
            }
        }

        private SwaggerGeneratedClient CreateClient(string url) => new(url, _client);
    }
}