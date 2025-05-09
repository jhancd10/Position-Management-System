using PositionManagement.Domain.Models;

namespace PositionManagement.Application.DTOs
{
    /// <summary>
    /// Represents a data transfer object for a position, including details such as title, description, location, status, 
    /// associated recruiter and department, budget, and closing date.
    /// </summary>
    public class PositionDto
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public PositionStatusEnum Status { get; set; }
        public Guid RecruiterId { get; set; }
        public Guid DepartmentId { get; set; }
        public decimal Budget { get; set; }
        public DateTime? ClosingDate { get; set; }

        public RecruiterDto Recruiter { get; set; }
        public DepartmentDto Department { get; set; }
    }
}
