namespace PositionManagement.Domain.Entities
{
    public class Department
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;

        public virtual ICollection<Position> Positions { get; set; } = [];
    }
}
