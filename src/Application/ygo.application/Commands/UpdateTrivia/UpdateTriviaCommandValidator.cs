using FluentValidation;

namespace ygo.application.Commands.UpdateTrivias
{
    public class UpdateTriviaCommandValidator : AbstractValidator<UpdateTriviaCommand>
    {
        public UpdateTriviaCommandValidator()
        {
            RuleFor(c => c.CardId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0);
        }
    }
}