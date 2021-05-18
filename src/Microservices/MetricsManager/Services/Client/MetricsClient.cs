using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MetricsManager.Services.Client.Requests;
using MetricsManager.Services.Client.Responses;

namespace MetricsManager.Services.Client
{
    public class MetricsClient : IMetricsClient
    {
        private readonly HttpClient _client;

        public MetricsClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CpuMetricResponse>> GetMetrics(CpuMetricsRequest request)
        {
            var uri = BuildUri(request.AgentUrl, "metrics/cpu", request.FromTime.ToUniversalTime(), request.ToTime);
            var response = await _client.GetAsync(uri).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return Array.Empty<CpuMetricResponse>();
            }

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var metrics = await JsonSerializer
                .DeserializeAsync<CpuMetricsByTimePeriodResponse>( stream, new(JsonSerializerDefaults.Web))
                .ConfigureAwait(false);

            return (metrics?.Metrics ?? new List<CpuMetricResponse>()).ToImmutableList();
        }

        public async Task<IEnumerable<DotnetMetricResponse>> GetMetrics(DotnetMetricsRequest request)
        {
            var uri = BuildUri(request.AgentUrl, "metrics/dotnet", request.FromTime.ToUniversalTime(), request.ToTime);
            var response = await _client.GetAsync(uri).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return Array.Empty<DotnetMetricResponse>();
            }

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var metrics = await JsonSerializer
                .DeserializeAsync<DotnetMetricsByTimePeriodResponse>(stream, new(JsonSerializerDefaults.Web))
                .ConfigureAwait(false);

            return (metrics?.Metrics ?? new List<DotnetMetricResponse>()).ToImmutableList();
        }

        public async Task<IEnumerable<HddMetricResponse>> GetMetrics(HddMetricsRequest request)
        {
            var uri = BuildUri(request.AgentUrl, "metrics/hdd", request.FromTime.ToUniversalTime(), request.ToTime);
            var response = await _client.GetAsync(uri).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return Array.Empty<HddMetricResponse>();
            }

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var metrics = await JsonSerializer
                .DeserializeAsync<HddMetricsByTimePeriodResponse>(stream, new(JsonSerializerDefaults.Web))
                .ConfigureAwait(false);

            return (metrics?.Metrics ?? new List<HddMetricResponse>()).ToImmutableList();
        }

        public async Task<IEnumerable<NetworkMetricResponse>> GetMetrics(NetworkMetricsRequest request)
        {
            var uri = BuildUri(request.AgentUrl, "metrics/network", request.FromTime.ToUniversalTime(), request.ToTime);
            var response = await _client.GetAsync(uri).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return Array.Empty<NetworkMetricResponse>();
            }

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var metrics = await JsonSerializer
                .DeserializeAsync<NetworkMetricsByTimePeriodResponse>(stream, new(JsonSerializerDefaults.Web))
                .ConfigureAwait(false);

            return (metrics?.Metrics ?? new List<NetworkMetricResponse>()).ToImmutableList();
        }

        public async Task<IEnumerable<RamMetricResponse>> GetMetrics(RamMetricsRequest request)
        {
            var uri = BuildUri(request.AgentUrl, "metrics/ram", request.FromTime.ToUniversalTime(), request.ToTime);
            var response = await _client.GetAsync(uri).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return Array.Empty<RamMetricResponse>();
            }

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var metrics = await JsonSerializer
                .DeserializeAsync<RamMetricsByTimePeriodResponse>(stream, new(JsonSerializerDefaults.Web))
                .ConfigureAwait(false);

            return (metrics?.Metrics ?? new List<RamMetricResponse>()).ToImmutableList();
        }

        private Uri BuildUri(string baseUri, string controller, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            return new($"{baseUri}/api/{controller}/from/{fromTime:O}/to/{toTime:O}");
        }
    }
}