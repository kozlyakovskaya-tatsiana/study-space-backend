using Domain.Guards;
using FluentValidation;

namespace Application.Validation;
public static class EmailRules
{
    public static IRuleBuilderOptions<T, string> EmailRulesSet<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(ValidationConstants.Email.MaxLength);
    }
}