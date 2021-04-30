namespace MetricsAgent.DataBase.Interfaces
{
    public interface IEntity<TId>
    {
        public TId Id { get; init; }
    }
}