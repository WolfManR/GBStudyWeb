using Common;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase.Interfaces
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric,int,int>
    {
        
    }
}