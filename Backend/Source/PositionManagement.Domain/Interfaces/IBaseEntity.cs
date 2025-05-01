namespace PositionManagement.Domain.Interfaces
{
    public interface IBaseEntity<TId>
    {
        TId EntityId { get; }
    }
}
