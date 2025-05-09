using MediatR;
using Microsoft.AspNetCore.Mvc;
using PositionManagement.Application.Commands.Departments;
using PositionManagement.Application.DTOs;
using PositionManagement.Application.Queries.Departments;

namespace PositionManagement.Api.Controllers
{
    /// <summary>
    /// Controller for managing department-related operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Retrieves all departments
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>List of departments</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
            // Sends a query to retrieve all departments
            => Ok(await _mediator.Send(new GetAllDepartmentsQuery(), cancellationToken));

        /// <summary>
        /// Creates a new department
        /// </summary>
        /// <param name="departmentDto">Data transfer object for the department</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Result of the creation operation</returns>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] DepartmentDto departmentDto,
            CancellationToken cancellationToken
        ) =>
            // Sends a command to create a new department
            Ok(await _mediator.Send(new CreateDepartmentCommand() { DepartmentDto = departmentDto }, cancellationToken));

        /// <summary>
        /// Updates an existing department
        /// </summary>
        /// <param name="id">Unique identifier of the department</param>
        /// <param name="DepartmentDto">Data transfer object for the department</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Result of the update operation</returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] DepartmentDto DepartmentDto,
            CancellationToken cancellationToken
        ) =>
            // Sends a command to update the specified department
            Ok(await _mediator.Send(new UpdateDepartmentCommand() { Id = id, DepartmentDto = DepartmentDto }, cancellationToken));

        /// <summary>
        /// Deletes a department
        /// </summary>
        /// <param name="id">Unique identifier of the department</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Result of the deletion operation</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            // Sends a command to delete the specified department
            await _mediator.Send(new DeleteDepartmentCommand() { Id = id }, cancellationToken);

            // Returns an OK response after deletion
            return Ok();
        }
    }
}
