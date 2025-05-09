using FluentValidation;
using PositionManagement.Application.Commands.Positions;

namespace PositionManagement.Application.Validators.Positions
{
    /// <summary>
    /// Validator for the DeletePositionCommand to ensure the command contains valid data
    /// </summary>
    public class DeletePositionCommandValidator : AbstractValidator<DeletePositionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the DeletePositionCommandValidator class
        /// </summary>
        public DeletePositionCommandValidator()
        {
            // Rule to ensure the Id property is not empty
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
