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

        public async Task<CpuMetricsByTimePeriodResponse> GetCpuMetrics(CpuMetricsRequest request)
        {
            var uri = BuildUri(request.AgentUrl, "cpu", request.FromTime, request.ToTime);
            var response = await _client.GetAsync(uri).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var metrics = await JsonSerializer.DeserializeAsync<IEnumerable<CpuMetricResponse>>(stream);

            return new()
            {
                Metrics = (metrics ?? new List<CpuMetricResponse>()).ToImmutableList()
            };
        }

        private Uri BuildUri(string baseUri, string controller, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            return new Uri($"{baseUri}/api/{controller}/from/{fromTime}/to/{toTime}");
        }
    }
}