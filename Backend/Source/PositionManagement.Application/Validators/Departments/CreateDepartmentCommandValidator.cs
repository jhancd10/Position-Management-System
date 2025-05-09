using FluentValidation;
using PositionManagement.Application.Commands.Departments;

namespace PositionManagement.Application.Validators.Departments
{
    /// <summary>
    /// Validator for the CreateDepartmentCommand, ensuring that the provided data meets the required rules
    /// </summary>
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateDepartmentCommandValidator class and defines validation rules
        /// </summary>
        public CreateDepartmentCommandValidator()
        {
            // Rule to ensure the Name property is not empty
            RuleFor(request => request.DepartmentDto.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be less than or equal 100 characters.");
        }
    }
}
