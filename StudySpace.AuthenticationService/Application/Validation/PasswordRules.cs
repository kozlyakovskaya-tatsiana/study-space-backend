using Domain.Guards;
using FluentValidation;

namespace Application.Validation;
public static class PasswordRules
{
    public static IRuleBuilderOptions<T, string> PasswordRulesSet<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty()
            .MinimumLength(ValidationConstants.Password.MinLength)
            .MaximumLength(ValidationConstants.Password.MaxLength);
    }
}
