using MapsterMapper;
using MediatR;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Interfaces.Services;
using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Commands.Recruiters;

/// <summary>
/// Command and handler for updating a recruiter in the system
/// </summary>
public record UpdateRecruiterCommand : IRequest<RecruiterDto>
{
    /// <summary>
    /// Unique identifier of the recruiter to update
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Data transfer object containing updated recruiter information
    /// </summary>
    public RecruiterDto RecruiterDto { get; init; }
}

/// <summary>
/// Handles the execution of the UpdateRecruiterCommand
/// </summary>
public class UpdateRecruiterCommandHandler(
    IMapper mapper,
    IRecruiterService recruiterService) : IRequestHandler<UpdateRecruiterCommand, RecruiterDto>
{
    private readonly IMapper _mapper = mapper; // Mapper instance for object mapping
    private readonly IRecruiterService _recruiterService = recruiterService; // Service for recruiter operations

    /// <summary>
    /// Handles the update recruiter command
    /// </summary>
    /// <param name="request">The command containing the recruiter update details</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests</param>
    /// <returns>Updated recruiter data transfer object</returns>
    public async Task<RecruiterDto> Handle(
        UpdateRecruiterCommand request,
        CancellationToken cancellationToken)
    {
        // Map the incoming DTO to a domain entity
        var recruiterToUpdate = _mapper.Map<Recruiter>(request.RecruiterDto);

        // Perform the update operation using the recruiter service
        var recruiterUpdated = await _recruiterService.UpdateAsync(recruiterToUpdate, cancellationToken);

        // Map the updated domain entity back to a DTO and return it
        return _mapper.Map<RecruiterDto>(recruiterUpdated);
    }
}
