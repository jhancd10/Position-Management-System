using FluentValidation;
using PositionManagement.Application.Commands.Recruiters;

namespace PositionManagement.Application.Validators.Recruiters
{
    /// <summary>
    /// Validator for the CreateRecruiterCommand to ensure the provided data is valid
    /// </summary>
    public class CreateRecruiterCommandValidator : AbstractValidator<CreateRecruiterCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateRecruiterCommandValidator class
        /// </summary>
        public CreateRecruiterCommandValidator()
        {
            // Rule to ensure the Email field is not empty
            RuleFor(request => request.RecruiterDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(100).WithMessage("Email must be less than or equal 100 characters.");

            // Rule to ensure the Cellphone field is not empty
            RuleFor(request => request.RecruiterDto.Cellphone)
                .NotEmpty().WithMessage("Cellphone is required.")
                .MaximumLength(15).WithMessage("Cellphone must be less than or equal 15 characters.");
        }
    }
}
