using FluentValidation;
using ygo.application.Enums;
using ygo.application.Models.Cards.Input;
using ygo.domain.Validation;

namespace ygo.application.Validations.Cards
{
    public class SpellCardValidator : AbstractValidator<CardInputModel>
    {
        public SpellCardValidator()
        {
            When(c => c.CardType == YgoCardType.Spell, () =>
            {
                RuleFor(c => c.Name)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .CardNameValidator();
            });
        }
    }
}