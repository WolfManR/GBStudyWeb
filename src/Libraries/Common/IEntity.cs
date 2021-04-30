namespace Common
{
    public interface IEntity<TId>
    {
        public TId Id { get; init; }
    }
}