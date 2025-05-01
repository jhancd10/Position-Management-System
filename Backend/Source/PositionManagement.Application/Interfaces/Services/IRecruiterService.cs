using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Interfaces.Services
{
    public interface IRecruiterService
    {
        Task<List<Recruiter>> GetAllAsync(bool includePositions = true);
        Task<Recruiter> GetByIdAsync(Guid id);
        Task<Recruiter> CreateAsync(Recruiter recruiter);
        Task<Recruiter> UpdateAsync(Recruiter recruiter);
        Task DeleteAsync(Guid id);
    }
}
