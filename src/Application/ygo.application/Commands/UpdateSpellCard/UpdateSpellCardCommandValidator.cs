using FluentValidation;
using ygo.domain.Validation;

namespace ygo.application.Commands.UpdateSpellCard
{
    public class UpdateSpellCardCommandValidator : AbstractValidator<UpdateSpellCardCommand>
    {
        public UpdateSpellCardCommandValidator()
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .CardNameValidator();
        }
    }
}