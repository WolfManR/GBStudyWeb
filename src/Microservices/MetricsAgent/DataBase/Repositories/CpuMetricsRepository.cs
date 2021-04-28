using System.Collections.Generic;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent.DataBase.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        
        
        /// <inheritdoc />
        public IList<CpuMetrics> Get() => null;

        /// <inheritdoc />
        public CpuMetrics Get(int id) => null;

        /// <inheritdoc />
        public int Create(CpuMetrics entity) => 0;
    }
}