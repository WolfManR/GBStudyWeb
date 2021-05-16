using System.Threading.Tasks;

using MetricsManager.Services.Client.Requests;
using MetricsManager.Services.Client.Responses;

namespace MetricsManager.Services.Client
{
    public interface IMetricsClient
    {
        Task<CpuMetricsByTimePeriodResponse> GetCpuMetrics(CpuMetricsRequest request);
    }
}