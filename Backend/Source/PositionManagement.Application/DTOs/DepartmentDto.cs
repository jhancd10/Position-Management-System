namespace PositionManagement.Application.DTOs
{
    /// <summary>
    /// Represents a department with its associated positions.
    /// </summary>
    public class DepartmentDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public ICollection<PositionDto> Positions { get; set; }
    }
}
