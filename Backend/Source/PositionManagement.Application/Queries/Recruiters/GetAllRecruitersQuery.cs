using MapsterMapper;
using MediatR;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Interfaces.Services;

namespace PositionManagement.Application.Queries.Recruiters;

/// <summary>
/// Query and handler to retrieve all recruiters from the system
/// </summary>
public record GetAllRecruitersQuery : IRequest<List<RecruiterDto>> { }

/// <summary>
/// Handles the GetAllRecruitersQuery to fetch and map recruiter data
/// </summary>
public class GetAllRecruitersQueryHandler(
    IMapper mapper,
    IRecruiterService recruiterService) : IRequestHandler<GetAllRecruitersQuery, List<RecruiterDto>>
{
    private readonly IMapper _mapper = mapper; // Mapper instance for object mapping
    private readonly IRecruiterService _recruiterService = recruiterService; // Service to interact with recruiter data

    /// <summary>
    /// Handles the query to retrieve all recruiters
    /// </summary>
    /// <param name="request">The query request</param>
    /// <param name="cancellationToken">Token to cancel the operation</param>
    /// <returns>List of recruiter DTOs</returns>
    public async Task<List<RecruiterDto>> Handle(
        GetAllRecruitersQuery request,
        CancellationToken cancellationToken)
    {
        // Fetch all recruiters asynchronously from the service
        var recruiters = await _recruiterService.GetAllAsync(cancellationToken: cancellationToken);

        // Map the recruiter entities to DTOs and return the result
        return _mapper.Map<List<RecruiterDto>>(recruiters);
    }
}
