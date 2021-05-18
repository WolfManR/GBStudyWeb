using System;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase.Interfaces
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric,int,int>
    {
        DateTimeOffset GetAgentLastMetricDate(int agentId);
    }
}