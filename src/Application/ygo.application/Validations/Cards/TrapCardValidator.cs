using FluentValidation;
using ygo.application.Enums;
using ygo.application.Models.Cards.Input;
using ygo.domain.Validation;

namespace ygo.application.Validations.Cards
{
    public class TrapCardValidator : AbstractValidator<CardInputModel>
    {
        public TrapCardValidator()
        {
            When(c => c.CardType == YgoCardType.Trap, () =>
            {
                RuleFor(c => c.Name)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .CardNameValidator();
            });
        }
    }
}