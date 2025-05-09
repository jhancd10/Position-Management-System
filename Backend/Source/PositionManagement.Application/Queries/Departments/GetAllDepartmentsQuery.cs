using MapsterMapper;
using MediatR;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Interfaces.Services;

namespace PositionManagement.Application.Queries.Departments;

/// <summary>
/// Query and handler to retrieve all departments as a list of DepartmentDto
/// </summary>
public record GetAllDepartmentsQuery : IRequest<List<DepartmentDto>> { }

/// <summary>
/// Handles the GetAllDepartmentsQuery to fetch and map department data
/// </summary>
public class GetAllDepartmentsQueryHandler(
    IMapper mapper,
    IDepartmentService departmentService) : IRequestHandler<GetAllDepartmentsQuery, List<DepartmentDto>>
{
    private readonly IMapper _mapper = mapper; // Mapper instance for object mapping
    private readonly IDepartmentService _departmentService = departmentService; // Service to interact with department data

    /// <summary>
    /// Handles the query to retrieve all departments
    /// </summary>
    /// <param name="request">The query request</param>
    /// <param name="cancellationToken">Token to cancel the operation</param>
    /// <returns>List of DepartmentDto</returns>
    public async Task<List<DepartmentDto>> Handle(
        GetAllDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        // Fetch all departments asynchronously using the service
        var departments = await _departmentService.GetAllAsync(cancellationToken: cancellationToken);

        // Map the fetched departments to a list of DepartmentDto
        return _mapper.Map<List<DepartmentDto>>(departments);
    }
}
