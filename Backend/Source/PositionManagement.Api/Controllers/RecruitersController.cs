using MediatR;
using Microsoft.AspNetCore.Mvc;
using PositionManagement.Application.Commands.Recruiters;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Queries.Recruiters;

namespace PositionManagement.Api.Controllers
{
    /// <summary>
    /// Controller for managing recruiter-related operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RecruitersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Retrieves all recruiters
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>List of recruiters</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
            // Sends a query to retrieve all recruiters
            => Ok(await _mediator.Send(new GetAllRecruitersQuery(), cancellationToken));

        /// <summary>
        /// Creates a new recruiter
        /// </summary>
        /// <param name="recruiterDto">Data transfer object containing recruiter details</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Result of the creation operation</returns>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] RecruiterDto recruiterDto,
            CancellationToken cancellationToken
        ) =>
            // Sends a command to create a new recruiter
            Ok(await _mediator.Send(new CreateRecruiterCommand() { RecruiterDto = recruiterDto }, cancellationToken));

        /// <summary>
        /// Updates an existing recruiter
        /// </summary>
        /// <param name="id">Unique identifier of the recruiter</param>
        /// <param name="RecruiterDto">Data transfer object containing updated recruiter details</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Result of the update operation</returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] RecruiterDto RecruiterDto,
            CancellationToken cancellationToken
        ) =>
            // Sends a command to update the recruiter with the specified ID
            Ok(await _mediator.Send(new UpdateRecruiterCommand() { Id = id, RecruiterDto = RecruiterDto }, cancellationToken));

        /// <summary>
        /// Deletes a recruiter
        /// </summary>
        /// <param name="id">Unique identifier of the recruiter</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Result of the deletion operation</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            // Sends a command to delete the recruiter with the specified ID
            await _mediator.Send(new DeleteRecruiterCommand() { Id = id }, cancellationToken);

            // Returns an OK response after deletion
            return Ok();
        }
    }
}
