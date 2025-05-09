using MapsterMapper;
using MediatR;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Interfaces.Services;

namespace PositionManagement.Application.Queries.Positions;

/// <summary>
/// Query and handler to retrieve all positions as a list of PositionDto objects
/// </summary>
public record GetAllPositionsQuery : IRequest<List<PositionDto>> { }

/// <summary>
/// Handles the GetAllPositionsQuery to fetch all positions and map them to DTOs
/// </summary>
public class GetAllPositionsQueryHandler(
    IMapper mapper,
    IPositionService positionService) : IRequestHandler<GetAllPositionsQuery, List<PositionDto>>
{
    private readonly IMapper _mapper = mapper; // Mapper instance to map domain models to DTOs
    private readonly IPositionService _positionService = positionService; // Service to interact with position data

    /// <summary>
    /// Handles the query to retrieve all positions
    /// </summary>
    /// <param name="request">The query request</param>
    /// <param name="cancellationToken">Token to cancel the operation</param>
    /// <returns>List of PositionDto objects</returns>
    public async Task<List<PositionDto>> Handle(
        GetAllPositionsQuery request,
        CancellationToken cancellationToken)
    {
        // Fetch all positions from the service
        var positions = await _positionService.GetAllAsync(cancellationToken: cancellationToken);

        // Map the fetched positions to a list of PositionDto objects
        return _mapper.Map<List<PositionDto>>(positions);
    }
}
