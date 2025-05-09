using MapsterMapper;
using MediatR;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Interfaces.Services;
using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Commands.Recruiters;

/// <summary>
/// Command and handler for creating a new recruiter
/// </summary>
public record CreateRecruiterCommand : IRequest<RecruiterDto>
{
    /// <summary>
    /// Data transfer object containing recruiter details
    /// </summary>
    public RecruiterDto RecruiterDto { get; init; }
}

/// <summary>
/// Handles the creation of a recruiter by processing the CreateRecruiterCommand
/// </summary>
public class CreateRecruiterCommandHandler(
    IMapper mapper,
    IRecruiterService recruiterService) : IRequestHandler<CreateRecruiterCommand, RecruiterDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly IRecruiterService _recruiterService = recruiterService;

    /// <summary>
    /// Handles the command to create a recruiter
    /// </summary>
    /// <param name="request">The command containing recruiter details</param>
    /// <param name="cancellationToken">Token to cancel the operation</param>
    /// <returns>The created recruiter as a DTO</returns>
    public async Task<RecruiterDto> Handle(
        CreateRecruiterCommand request,
        CancellationToken cancellationToken)
    {
        // Map the RecruiterDto to a Recruiter entity
        var recruiterToCreate = _mapper.Map<Recruiter>(request.RecruiterDto);

        // Call the service to create the recruiter asynchronously
        var recruiterCreated = await _recruiterService.CreateAsync(recruiterToCreate, cancellationToken);

        // Map the created Recruiter entity back to a RecruiterDto and return it
        return _mapper.Map<RecruiterDto>(recruiterCreated);
    }
}
