using FluentValidation;
using PositionManagement.Application.Commands.Recruiters;

namespace PositionManagement.Application.Validators.Recruiters
{
    /// <summary>
    /// Validator for the UpdateRecruiterCommand, ensuring that the command's properties meet the required validation rules
    /// </summary>
    public class UpdateRecruiterCommandValidator : AbstractValidator<UpdateRecruiterCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRecruiterCommandValidator"/> class and defines validation rules
        /// </summary>
        public UpdateRecruiterCommandValidator()
        {
            /* Rule to ensure the Id property is not empty and
             * the Id matches the RecruiterDto.Id */

            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must((request, id) => id == request.RecruiterDto.Id)
                .WithMessage("Id must match RecruiterDto.Id.");

            // Rule to ensure the Email property is not empty
            RuleFor(request => request.RecruiterDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(100).WithMessage("Email must be less than or equal 100 characters.");

            // Rule to ensure the Cellphone property is not empty
            RuleFor(request => request.RecruiterDto.Cellphone)
                .NotEmpty().WithMessage("Cellphone is required.")
                .MaximumLength(15).WithMessage("Cellphone must be less than or equal 15 characters.");
        }
    }
}
