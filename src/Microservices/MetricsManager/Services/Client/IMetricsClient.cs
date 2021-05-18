using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using MetricsManager.Services.Client.Requests;
using MetricsManager.Services.Client.Responses;

namespace MetricsManager.Services.Client
{
    public interface IMetricsClient
    {
        Task<IEnumerable<CpuMetricResponse>> GetMetrics(CpuMetricsRequest request);
    }
}