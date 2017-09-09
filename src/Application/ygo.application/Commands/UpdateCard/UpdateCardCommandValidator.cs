using FluentValidation;

namespace ygo.application.Commands.UpdateCard
{
    public class UpdateCardCommandValidator : AbstractValidator<UpdateCardCommand>
    {
        public UpdateCardCommandValidator()
        {
            RuleFor(c => c.CardType)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .IsInEnum();
        }
    }

}