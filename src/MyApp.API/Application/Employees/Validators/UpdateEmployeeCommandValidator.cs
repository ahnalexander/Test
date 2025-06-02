using FluentValidation;
using MyApp.Application.Common.CommandHandlers;
using MyApp.Application.Employees.Models;
using MyApp.Domain;

namespace MyApp.Application.Employees.Validators;

public class UpdateEmployeeCommandValidator : AbstractValidator<BaseUpdateCommand<Employee, UpdateEmployeeModel>>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Data.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(256).WithMessage("Email cannot exceed 256 characters");

        RuleFor(x => x.Data.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Invalid employee ID");
    }
}