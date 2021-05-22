using Common;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent.DataBase.Interfaces
{
    public interface IDotnetMetricsRepository : IRepository<DotnetMetric,int>
    {
        
    }
}