using MediatR;
using PositionManagement.Application.Interfaces.Services;

namespace PositionManagement.Application.Commands.Positions;

/// <summary>
/// Command to delete a position by its unique identifier
/// </summary>
public record DeletePositionCommand : IRequest
{
    /// <summary>
    /// The unique identifier of the position to delete
    /// </summary>
    public Guid Id { get; init; }
}

/// <summary>
/// Handles the deletion of a position by processing the DeletePositionCommand
/// </summary>
public class DeletePositionCommandHandler(
    IPositionService positionService) : IRequestHandler<DeletePositionCommand>
{
    private readonly IPositionService _positionService = positionService;

    /// <summary>
    /// Processes the command to delete a position
    /// </summary>
    /// <param name="request">The command containing the position ID to delete</param>
    /// <param name="cancellationToken">Token to cancel the operation</param>
    public async Task Handle(
        DeletePositionCommand request,
        CancellationToken cancellationToken)
    {
        // Calls the service to delete the position by its ID
        await _positionService.DeleteAsync(request.Id, cancellationToken);
    }
}
