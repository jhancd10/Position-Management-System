namespace PositionManagement.Application.DTOs
{
    /// <summary>
    /// Represents a data transfer object for a recruiter, including their details and associated positions
    /// </summary>
    public class RecruiterDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        
        public virtual ICollection<PositionDto> Positions { get; set; }
    }
}
