using System.Collections.Generic;
using MetricsManager.DataBase.Models;

namespace MetricsManager.DataBase.Interfaces
{
    public interface IAgentsRepository
    {
        void Create(AgentInfo agent);
        IList<AgentInfo> Get();
        AgentInfo GetById(int id);
    }
}