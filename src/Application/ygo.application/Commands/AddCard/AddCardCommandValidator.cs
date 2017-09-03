using FluentValidation;

namespace ygo.application.Commands.AddCard
{
    public class AddCardCommandValidator : AbstractValidator<AddCardCommand>
    {
        public AddCardCommandValidator()
        {
            RuleFor(c => c.CardType)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .IsInEnum();
        }
    }

}