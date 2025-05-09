using MapsterMapper;
using MediatR;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Interfaces.Services;
using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Commands.Positions;

/// <summary>
/// Command and handler for updating a position in the system
/// </summary>
public record UpdatePositionCommand : IRequest<PositionDto>
{
    /// <summary>
    /// Identifier of the position to update
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Data transfer object containing updated position details
    /// </summary>
    public PositionDto PositionDto { get; init; }
}

/// <summary>
/// Handles the execution of the UpdatePositionCommand
/// </summary>
public class UpdatePositionCommandHandler(
    IMapper mapper,
    IPositionService positionService) : IRequestHandler<UpdatePositionCommand, PositionDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly IPositionService _positionService = positionService;

    /// <summary>
    /// Handles the update position command by mapping the DTO to the entity, updating it, and returning the updated DTO
    /// </summary>
    /// <param name="request">The update position command containing the position details</param>
    /// <param name="cancellationToken">Token to cancel the operation</param>
    /// <returns>Updated position DTO</returns>
    public async Task<PositionDto> Handle(
        UpdatePositionCommand request,
        CancellationToken cancellationToken)
    {
        // Map the incoming PositionDto to a Position entity
        var positionToUpdate = _mapper.Map<Position>(request.PositionDto);

        // Update the position in the service layer
        var positionUpdated = await _positionService.UpdateAsync(positionToUpdate, cancellationToken);

        // Map the updated Position entity back to a PositionDto and return it
        return _mapper.Map<PositionDto>(positionUpdated);
    }
}
