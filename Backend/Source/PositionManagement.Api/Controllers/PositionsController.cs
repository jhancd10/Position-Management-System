using MediatR;
using Microsoft.AspNetCore.Mvc;
using PositionManagement.Application.Commands.Positions;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Queries.Positions;

namespace PositionManagement.Api.Controllers
{
    /// <summary>
    /// Controller for managing position-related operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Retrieves all positions
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>List of all positions</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
            // Sends a query to retrieve all positions
            => Ok(await _mediator.Send(new GetAllPositionsQuery(), cancellationToken));

        /// <summary>
        /// Retrieves a position by its ID
        /// </summary>
        /// <param name="id">Unique identifier of the position</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Details of the specified position</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken
        ) =>
            // Sends a query to retrieve a position by its ID
            Ok(await _mediator.Send(new GetPositionByIdQuery() { Id = id }, cancellationToken));

        /// <summary>
        /// Creates a new position
        /// </summary>
        /// <param name="positionDto">Data transfer object containing position details</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Result of the creation operation</returns>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] PositionDto positionDto,
            CancellationToken cancellationToken
        ) =>
            // Sends a command to create a new position
            Ok(await _mediator.Send(new CreatePositionCommand() { PositionDto = positionDto }, cancellationToken));

        /// <summary>
        /// Updates an existing position
        /// </summary>
        /// <param name="id">Unique identifier of the position to update</param>
        /// <param name="PositionDto">Data transfer object containing updated position details</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Result of the update operation</returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] PositionDto PositionDto,
            CancellationToken cancellationToken
        ) =>
            // Sends a command to update an existing position
            Ok(await _mediator.Send(new UpdatePositionCommand() { Id = id, PositionDto = PositionDto }, cancellationToken));

        /// <summary>
        /// Deletes a position by its ID
        /// </summary>
        /// <param name="id">Unique identifier of the position to delete</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Result of the deletion operation</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            // Sends a command to delete a position by its ID
            await _mediator.Send(new DeletePositionCommand() { Id = id }, cancellationToken);

            // Returns a success response
            return Ok();
        }
    }
}
