namespace MetricsAgent.DataBase.Models
{
    public interface IEntity<TScu>
    {
        public TScu Scu { get; init; }
    }
}