namespace MetricsAgent.DataBase.Models
{
    public interface IEntity<TId>
    {
        public TId Id { get; init; }
    }
}