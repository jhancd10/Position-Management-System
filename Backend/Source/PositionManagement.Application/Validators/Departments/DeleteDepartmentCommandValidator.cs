using FluentValidation;
using PositionManagement.Application.Commands.Departments;

namespace PositionManagement.Application.Validators.Departments
{
    /// <summary>
    /// Validator for the DeleteDepartmentCommand to ensure the command's properties meet the required conditions
    /// </summary>
    public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDepartmentCommandValidator"/> class
        /// </summary>
        public DeleteDepartmentCommandValidator()
        {
            // Rule to ensure the Id property is not empty
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
