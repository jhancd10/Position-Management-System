using FluentValidation;
using PositionManagement.Application.Commands.Positions;
using PositionManagement.Domain.Models;

namespace PositionManagement.Application.Validators.Positions
{
    /// <summary>
    /// Validator for the CreatePositionCommand, ensuring all required fields are valid and meet business rules
    /// </summary>
    public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreatePositionCommandValidator class and defines validation rules
        /// </summary>
        public CreatePositionCommandValidator()
        {
            // Rule to ensure Title is not empty and does not exceed 100 characters
            RuleFor(request => request.PositionDto.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title must be less than or equal 100 characters");

            // Rule to ensure Description is not empty and does not exceed 1000 characters
            RuleFor(request => request.PositionDto.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Description must be less than or equal 1000 characters");

            // Rule to ensure Location is not empty
            RuleFor(request => request.PositionDto.Location).NotEmpty().WithMessage("Location is required");

            // Rule to ensure Status is not empty and is a valid enum value
            RuleFor(request => request.PositionDto.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => Enum.IsDefined(typeof(PositionStatusEnum), status))
                .WithMessage("Invalid status value");

            // Rule to ensure RecruiterId is not empty
            RuleFor(request => request.PositionDto.RecruiterId)
                .NotEmpty().WithMessage("RecruiterId is required");

            // Rule to ensure DepartmentId is not empty
            RuleFor(request => request.PositionDto.DepartmentId)
                .NotEmpty().WithMessage("DepartmentId is required");

            // Rule to ensure Budget is greater than zero
            RuleFor(request => request.PositionDto.Budget)
                .GreaterThan(0).WithMessage("Budget must be greater than zero");

            // Rule to ensure ClosingDate is either null or a future date
            RuleFor(request => request.PositionDto.ClosingDate)
                .Must(closingDate => closingDate == null || closingDate > DateTime.UtcNow)
                .WithMessage("Closing date must be in the future");
        }
    }
}
