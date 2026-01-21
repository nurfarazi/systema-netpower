using FluentValidation;
using NetPower.Application.Features.Users.DTOs;

namespace NetPower.Application.Features.Users.Validators;

/// <summary>
/// Validator for GetUsersQuery.
/// </summary>
public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page must be at least 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100)
            .WithMessage("PageSize must be between 1 and 100.");

        RuleFor(x => x.Search)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Search))
            .WithMessage("Search term must not exceed 100 characters.");
    }
}
