using FluentValidation;

namespace ygo.domain.Validation
{
    public static class CardValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> CardNameValidator<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                    .NotNull()
                    .NotEmpty()
                    .Length(1, 255);
        }
    }
}