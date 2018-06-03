using FluentValidation;

namespace ygo.application.Commands.UpdateRulings
{
    public class UpdateRulingCommandValidator : AbstractValidator<UpdateRulingCommand>
    {
        public UpdateRulingCommandValidator()
        {
            RuleFor(c => c.CardId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0);
        }
    }
}