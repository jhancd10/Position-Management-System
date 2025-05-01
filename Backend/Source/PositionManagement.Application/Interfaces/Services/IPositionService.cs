using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Interfaces.Services
{
    public interface IPositionService
    {
        Task<List<Position>> GetAllAsync(bool includeDetails = true);
        Task<Position> GetByIdAsync(Guid id);
        Task<Position> CreateAsync(Position positions);
        Task<Position> UpdateAsync(Position positions);
        Task DeleteAsync(Guid id);
    }
}
