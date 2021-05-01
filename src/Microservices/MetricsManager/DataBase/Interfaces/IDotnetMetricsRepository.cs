using Common;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase.Interfaces
{
    public interface IDotnetMetricsRepository : IRepository<DotnetMetric,int>
    {
        
    }
}