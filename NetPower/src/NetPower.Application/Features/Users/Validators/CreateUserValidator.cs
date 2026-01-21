using FluentValidation;
using NetPower.Application.Features.Users.DTOs;

namespace NetPower.Application.Features.Users.Validators;

/// <summary>
/// Validator for CreateUserDto.
/// </summary>
public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(200)
            .WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.")
            .MaximumLength(200)
            .WithMessage("Email must not exceed 200 characters.");
    }
}
