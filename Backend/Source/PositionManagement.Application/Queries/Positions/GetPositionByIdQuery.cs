using MapsterMapper;
using MediatR;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Interfaces.Services;

namespace PositionManagement.Application.Queries.Positions;

/// <summary>
/// Query and handler to retrieve a position by its unique identifier
/// </summary>
public record GetPositionByIdQuery : IRequest<PositionDto>
{
    /// <summary>
    /// Unique identifier of the position to retrieve
    /// </summary>
    public Guid Id { get; init; }
}

/// <summary>
/// Handles the logic to process the GetPositionByIdQuery and return the corresponding position
/// </summary>
public class GetPositionByIdQueryHandler(
    IMapper mapper,
    IPositionService positionService) : IRequestHandler<GetPositionByIdQuery, PositionDto>
{
    private readonly IMapper _mapper = mapper; // Mapper instance to map domain models to DTOs
    private readonly IPositionService _positionService = positionService; // Service to interact with position data

    /// <summary>
    /// Handles the query to retrieve a position by its ID
    /// </summary>
    /// <param name="request">The query containing the position ID</param>
    /// <param name="cancellationToken">Token to cancel the operation if needed</param>
    /// <returns>The position data mapped to a PositionDto</returns>
    public async Task<PositionDto> Handle(
        GetPositionByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Retrieve the position from the service using the provided ID
        var position = await _positionService.GetByIdAsync(request.Id, cancellationToken);

        // Map the retrieved position to a PositionDto and return it
        return _mapper.Map<PositionDto>(position);
    }
}
