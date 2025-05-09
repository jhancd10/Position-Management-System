using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PositionManagement.Api.Controllers;
using PositionManagement.Application.Commands.Positions;
using PositionManagement.Application.DTOs;
using PositionManagement.Domain.Models;

namespace Test.PositionManagement.Api.Positions;

/// <summary>
/// Unit tests for the PositionsController, ensuring proper behavior of position-related operations
/// </summary>
[TestClass]
public class PositionsControllerTests
{
    // Mock instance of IMediator
    private Mock<IMediator> _mediatorMock;

    // Instance of PositionsController being tested
    private PositionsController _positionsController;

    /// <summary>
    /// Sets up the test environment by initializing mocks and the controller
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        // Initialize the mock mediator
        _mediatorMock = new Mock<IMediator>();

        // Initialize the controller with the mock mediator
        _positionsController = new PositionsController(_mediatorMock.Object);
    }

    /// <summary>
    /// Tests that the Create method returns an OkObjectResult when the command is handled successfully
    /// </summary>
    [TestMethod]
    public async Task Create_ReturnsOkObjectResult_WhenCommandHandledSuccessfully()
    {
        /* Step 1 -> Arrange: */

        // Prepare the input DTO for creating a position
        var positionToCreateDto = new PositionDto()
        {
            Title = "Position From Test",
            Description = "Position's description from test",
            Location = "Colombia",
            Status = PositionStatusEnum.Open,
            RecruiterId = Guid.NewGuid(),
            DepartmentId = Guid.NewGuid(),
            Budget = 4000,
            ClosingDate = DateTime.UtcNow.AddDays(30),
        };

        // Prepare the expected output DTO after the position is created
        var positionCreatedDto = new PositionDto()
        {
            Id = Guid.NewGuid(),
            Title = positionToCreateDto.Title,
            Description = positionToCreateDto.Description,
            Location = positionToCreateDto.Location,
            Status = positionToCreateDto.Status,
            Budget = positionToCreateDto.Budget,
            ClosingDate = positionToCreateDto.ClosingDate,
            DepartmentId = positionToCreateDto.DepartmentId,
            RecruiterId = positionToCreateDto.RecruiterId
        };

        // Create the command to be sent to the mediator
        var createPositionCommand = new CreatePositionCommand() { PositionDto = positionToCreateDto };

        // Setup the mock mediator to return the expected output DTO
        _mediatorMock.Setup(
            m => m.Send(
                It.Is<CreatePositionCommand>(command => command.PositionDto == positionToCreateDto),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(positionCreatedDto);


        /* Step 2 -> Act: */

        // Call the Create method on the controller
        var result = await _positionsController.Create(positionToCreateDto, CancellationToken.None);


        /* Step 3 -> Asserts and Verifies */

        // Verify the result is of type OkObjectResult
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        // Verify the status code and value of the result
        var createdResult = result as OkObjectResult;
        Assert.AreEqual(StatusCodes.Status200OK, createdResult?.StatusCode);
        Assert.AreEqual(positionCreatedDto, createdResult?.Value);

        // Verify the mediator's Send method was called exactly once
        _mediatorMock.Verify(m => m.Send(It.IsAny<CreatePositionCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
