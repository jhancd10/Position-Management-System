using FluentValidation;
using PositionManagement.Application.Commands.Recruiters;

namespace PositionManagement.Application.Validators.Recruiters
{
    /// <summary>
    /// Validator for the DeleteRecruiterCommand to ensure the command's properties meet the required criteria
    /// </summary>
    public class DeleteRecruiterCommandValidator : AbstractValidator<DeleteRecruiterCommand>
    {
        /// <summary>
        /// Initializes a new instance of the DeleteRecruiterCommandValidator class
        /// </summary>
        public DeleteRecruiterCommandValidator()
        {
            // Rule to ensure the Id property is not empty
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
