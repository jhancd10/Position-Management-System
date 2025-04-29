using PositionManagement.Domain.Models;

namespace PositionManagement.Domain.Entities
{
    public class Position
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        public PositionStatusEnum Status { get; set; }
        public Guid RecruiterId { get; set; }
        public Guid DepartmentId { get; set; }
        public decimal Budget { get; set; }
        public DateTime? ClosingDate { get; set; }

        public virtual Recruiter Recruiter { get; set; } = null!;
        public virtual Department Department { get; set; } = null!;
    }
}
