using FluentValidation;
using ygo.domain.Validation;

namespace ygo.application.Commands.AddMonsterCard
{
    public class AddMonsterCardCommandValidator : AbstractValidator<AddMonsterCardCommand>
    {
        private const int MaxCardLevel = 12;
        private const int MaxCardRank = 12;
        private const int MaxAtk = 10000;
        private const int MaxDef = 10000;

        public AddMonsterCardCommandValidator()
        {
            DefaultValidatorOptions.Cascade(RuleFor(c => c.Name), CascadeMode.StopOnFirstFailure)
                .CardNameValidator();

            DefaultValidatorOptions.Cascade(RuleFor(c => c.CardLevel), CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .LessThanOrEqualTo(MaxCardLevel)
                .When(c => c.CardLevel.HasValue);

            DefaultValidatorOptions.Cascade(RuleFor(c => c.CardRank), CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .LessThanOrEqualTo(MaxCardRank)
                .When(c => c.CardRank.HasValue);

            DefaultValidatorOptions.When(RuleFor(c => c.Atk)
                    .GreaterThan(0)
                    .LessThanOrEqualTo(MaxAtk), c => c.Atk.HasValue);

            DefaultValidatorOptions.When(RuleFor(c => c.Def)
                    .GreaterThan(0)
                    .LessThanOrEqualTo(MaxDef), c => c.Def.HasValue);

            RuleFor(c => c.AttributeId)
                .GreaterThan(0);

            DefaultValidatorExtensions.NotNull(RuleFor(c => c.SubCategoryIds))
                .NotEmpty();
        }
    }
}