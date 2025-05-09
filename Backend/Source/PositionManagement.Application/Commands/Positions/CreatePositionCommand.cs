using MapsterMapper;
using MediatR;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Interfaces.Services;
using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Commands.Positions;

/// <summary>
/// Command and handler for creating a new position
/// </summary>
public record CreatePositionCommand : IRequest<PositionDto>
{
    /// <summary>
    /// Data transfer object containing position details
    /// </summary>
    public PositionDto PositionDto { get; init; }
}

/// <summary>
/// Handles the creation of a new position by processing the CreatePositionCommand
/// </summary>
public class CreatePositionCommandHandler(
    IMapper mapper,
    IPositionService positionService) : IRequestHandler<CreatePositionCommand, PositionDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly IPositionService _positionService = positionService;

    /// <summary>
    /// Handles the command to create a new position
    /// </summary>
    /// <param name="request">The command containing position details</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests</param>
    /// <returns>The created position as a DTO</returns>
    public async Task<PositionDto> Handle(
        CreatePositionCommand request,
        CancellationToken cancellationToken)
    {
        // Map the incoming PositionDto to a Position entity
        var positionToCreate = _mapper.Map<Position>(request.PositionDto);

        // Create the position using the service
        var positionCreated = await _positionService.CreateAsync(positionToCreate, cancellationToken);

        // Map the created Position entity back to a PositionDto and return it
        return _mapper.Map<PositionDto>(positionCreated);
    }
}
