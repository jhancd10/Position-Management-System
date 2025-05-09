using MediatR;
using PositionManagement.Application.Interfaces.Services;

namespace PositionManagement.Application.Commands.Departments;

/// <summary>
/// Command to delete a department by its unique identifier
/// </summary>
public record DeleteDepartmentCommand : IRequest
{
    /// <summary>
    /// The unique identifier of the department to delete
    /// </summary>
    public Guid Id { get; init; }
}

/// <summary>
/// Handles the execution of the DeleteDepartmentCommand
/// </summary>
public class DeleteDepartmentCommandHandler(
    IDepartmentService departmentService) : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly IDepartmentService _departmentService = departmentService;

    /// <summary>
    /// Executes the command to delete a department
    /// </summary>
    /// <param name="request">The command containing the department ID</param>
    /// <param name="cancellationToken">Token to cancel the operation</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task Handle(
        DeleteDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        // Calls the service to delete the department by its ID
        await _departmentService.DeleteAsync(request.Id, cancellationToken);
    }
}
