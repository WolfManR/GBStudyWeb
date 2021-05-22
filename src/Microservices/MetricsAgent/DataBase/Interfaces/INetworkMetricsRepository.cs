using Common;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent.DataBase.Interfaces
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric,int>
    {
        
    }
}