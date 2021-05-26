using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using MetricsManager.Services.Client.Requests;
using MetricsManager.Services.SwaggerClient;

namespace MetricsManager.Services.Client
{
    public interface IMetricsClient
    {
        Task<IEnumerable<CpuMetricResponse>> GetMetrics(CpuMetricsRequest request);
        Task<IEnumerable<DotnetMetricResponse>> GetMetrics(DotnetMetricsRequest request);
        Task<IEnumerable<HddMetricResponse>> GetMetrics(HddMetricsRequest request);
        Task<IEnumerable<NetworkMetricResponse>> GetMetrics(NetworkMetricsRequest request);
        Task<IEnumerable<RamMetricResponse>> GetMetrics(RamMetricsRequest request);
    }
}