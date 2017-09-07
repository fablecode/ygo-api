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
            RuleFor(c => c.Name).Cascade(CascadeMode.StopOnFirstFailure)
                .CardNameValidator();

            RuleFor(c => c.CardLevel).Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .LessThanOrEqualTo(MaxCardLevel)
                .When(c => c.CardLevel.HasValue);

            RuleFor(c => c.CardRank).Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .LessThanOrEqualTo(MaxCardRank)
                .When(c => c.CardRank.HasValue);

            RuleFor(c => c.Atk)
                .GreaterThan(0)
                .LessThanOrEqualTo(MaxAtk).When(c => c.Atk.HasValue);

            RuleFor(c => c.Def)
                .GreaterThan(0)
                .LessThanOrEqualTo(MaxDef).When(c => c.Def.HasValue);

            RuleFor(c => c.AttributeId)
                .GreaterThan(0);

            RuleFor(c => c.SubCategoryIds)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.CardNumber)
                .GreaterThan(0)
                .When(c => c.CardNumber.HasValue);
        }
    }
}