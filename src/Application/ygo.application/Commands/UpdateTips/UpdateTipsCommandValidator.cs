using FluentValidation;

namespace ygo.application.Commands.UpdateTips
{
    public class UpdateTipsCommandValidator : AbstractValidator<UpdateTipsCommand>
    {
        public UpdateTipsCommandValidator()
        {
            RuleFor(c => c.CardId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0);
        }
    }
}