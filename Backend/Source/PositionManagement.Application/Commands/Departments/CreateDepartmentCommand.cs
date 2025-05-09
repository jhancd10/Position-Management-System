using MapsterMapper;
using MediatR;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Interfaces.Services;
using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Commands.Departments;

/// <summary>
/// Command and handler for creating a new department
/// </summary>
public record CreateDepartmentCommand : IRequest<DepartmentDto>
{
    /// <summary>
    /// Data transfer object containing department details
    /// </summary>
    public DepartmentDto DepartmentDto { get; init; }
}

/// <summary>
/// Handles the creation of a new department
/// </summary>
public class CreateDepartmentCommandHandler(
    IMapper mapper,
    IDepartmentService departmentService) : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly IDepartmentService _departmentService = departmentService;

    /// <summary>
    /// Handles the creation of a department
    /// </summary>
    /// <param name="request">The command containing department data</param>
    /// <param name="cancellationToken">Token to cancel the operation</param>
    /// <returns>The created department as a DTO</returns>
    public async Task<DepartmentDto> Handle(
        CreateDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        // Map the incoming DTO to a domain entity
        var departmentToCreate = _mapper.Map<Department>(request.DepartmentDto);

        // Call the service to create the department asynchronously
        var departmentCreated = await _departmentService.CreateAsync(departmentToCreate, cancellationToken);

        // Map the created domain entity back to a DTO and return it
        return _mapper.Map<DepartmentDto>(departmentCreated);
    }
}
