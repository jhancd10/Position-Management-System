using FluentValidation;
using PositionManagement.Application.Commands.Departments;

namespace PositionManagement.Application.Validators.Departments
{
    /// <summary>
    /// Validator for the UpdateDepartmentCommand, ensuring that the command's properties meet the required validation rules
    /// </summary>
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateDepartmentCommandValidator class and defines validation rules
        /// </summary>
        public UpdateDepartmentCommandValidator()
        {
            /* Rule to ensure the Id property is not empty and 
             * the Id matches the DepartmentDto.Id */

            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must((request, id) => id == request.DepartmentDto.Id)
                .WithMessage("Id must match DepartmentDto.Id.");

            // Rule to ensure the Name property of DepartmentDto is not empty
            RuleFor(request => request.DepartmentDto.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be less than or equal 100 characters.");
        }
    }
}
