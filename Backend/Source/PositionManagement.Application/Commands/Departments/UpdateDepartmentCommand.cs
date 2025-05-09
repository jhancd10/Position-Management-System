using MapsterMapper;
using MediatR;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Interfaces.Services;
using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Commands.Departments;

/// <summary>
/// Command and handler for updating a department
/// </summary>
public record UpdateDepartmentCommand : IRequest<DepartmentDto>
{
    /// <summary>
    /// Identifier of the department to update
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Data transfer object containing updated department information
    /// </summary>
    public DepartmentDto DepartmentDto { get; init; }
}

/// <summary>
/// Handles the update operation for a department
/// </summary>
public class UpdateDepartmentCommandHandler(
    IMapper mapper,
    IDepartmentService departmentService) : IRequestHandler<UpdateDepartmentCommand, DepartmentDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly IDepartmentService _departmentService = departmentService;

    /// <summary>
    /// Handles the update department command
    /// </summary>
    /// <param name="request">The update department command</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests</param>
    /// <returns>Updated department data transfer object</returns>
    public async Task<DepartmentDto> Handle(
        UpdateDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        // Map the incoming DTO to a domain entity
        var departmentToUpdate = _mapper.Map<Department>(request.DepartmentDto);

        // Perform the update operation using the service
        var departmentUpdated = await _departmentService.UpdateAsync(departmentToUpdate, cancellationToken);

        // Map the updated domain entity back to a DTO and return it
        return _mapper.Map<DepartmentDto>(departmentUpdated);
    }
}
