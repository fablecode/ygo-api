using FluentValidation;
using ygo.domain.Validation;

namespace ygo.application.Commands.AddSpellCard
{
    public class AddSpellCardCommandValidator : AbstractValidator<AddSpellCardCommand>
    {
        public AddSpellCardCommandValidator()
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .CardNameValidator();
        }
    }
}