using FluentValidation;
using PositionManagement.Application.Commands.Positions;
using PositionManagement.Domain.Models;

namespace PositionManagement.Application.Validators.Positions
{
    /// <summary>
    /// Validator for the UpdatePositionCommand. Ensures that all required fields are provided and meet the specified validation rules
    /// </summary>
    public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePositionCommandValidator"/> class and defines validation rules for the UpdatePositionCommand
        /// </summary>
        public UpdatePositionCommandValidator()
        {
            /* Rule to ensure the Id is not empty and
             * the Id matches the PositionDto.Id */

            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must((request, id) => id == request.PositionDto.Id)
                .WithMessage("Id must match PositionDto.Id.");

            // Rule to ensure the Title is not empty and has a maximum length of 100 characters
            RuleFor(request => request.PositionDto.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must be less than or equal 100 characters.");

            // Rule to ensure the Description is not empty and has a maximum length of 1000 characters
            RuleFor(request => request.PositionDto.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must be less than or equal 1000 characters.");

            // Rule to ensure the Location is not empty
            RuleFor(request => request.PositionDto.Location)
                .NotEmpty().WithMessage("Location is required.");

            // Rule to ensure the Status is not empty and is a valid enum value
            RuleFor(request => request.PositionDto.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => Enum.IsDefined(typeof(PositionStatusEnum), status))
                .WithMessage("Invalid status value.");

            // Rule to ensure the RecruiterId is not empty
            RuleFor(request => request.PositionDto.RecruiterId)
                .NotEmpty().WithMessage("RecruiterId is required.");

            // Rule to ensure the DepartmentId is not empty
            RuleFor(request => request.PositionDto.DepartmentId)
                .NotEmpty().WithMessage("DepartmentId is required.");

            // Rule to ensure the Budget is greater than zero
            RuleFor(request => request.PositionDto.Budget)
                .GreaterThan(0).WithMessage("Budget must be greater than zero.");

            // Rule to ensure the ClosingDate is either null or in the future
            RuleFor(request => request.PositionDto.ClosingDate)
                .Must(closingDate => closingDate == null || closingDate > DateTime.UtcNow)
                .WithMessage("Closing date must be in the future.");
        }
    }
}
