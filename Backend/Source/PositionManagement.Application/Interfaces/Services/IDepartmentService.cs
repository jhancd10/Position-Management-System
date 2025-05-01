using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllAsync(bool includePositions = true);
        Task<Department> GetByIdAsync(Guid id);
        Task<Department> CreateAsync(Department department);
        Task<Department> UpdateAsync(Department department);
        Task DeleteAsync(Guid id);
    }
}
