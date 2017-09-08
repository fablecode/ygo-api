using FluentValidation;
using ygo.domain.Validation;

namespace ygo.application.Commands.AddTrapCard
{
    public class AddTrapCardCommandValidator : AbstractValidator<AddTrapCardCommand>
    {
        public AddTrapCardCommandValidator()
        {
            DefaultValidatorOptions.Cascade(RuleFor(c => c.Name), CascadeMode.StopOnFirstFailure)
                .CardNameValidator();
        }
    }
}