using MediatR;
using PositionManagement.Application.Interfaces.Services;

namespace PositionManagement.Application.Commands.Recruiters;

/// <summary>
/// Command to delete a recruiter by their unique identifier.
/// </summary>
public record DeleteRecruiterCommand : IRequest
{
    /// <summary>
    /// The unique identifier of the recruiter to delete.
    /// </summary>
    public Guid Id { get; init; }
}

/// <summary>
/// Handles the deletion of a recruiter by invoking the appropriate service method.
/// </summary>
public class DeletePositionCommandHandler(
    IPositionService positionService) : IRequestHandler<DeleteRecruiterCommand>
{
    private readonly IPositionService _positionService = positionService;

    /// <summary>
    /// Handles the delete recruiter command by calling the service to delete the recruiter.
    /// </summary>
    /// <param name="request">The delete recruiter command containing the recruiter ID.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Handle(
        DeleteRecruiterCommand request,
        CancellationToken cancellationToken)
    {
        await _positionService.DeleteAsync(request.Id, cancellationToken);
    }
}
