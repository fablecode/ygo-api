using FluentValidation;
using ygo.application.Commands.AddTrapCard;
using ygo.domain.Validation;

namespace ygo.application.Commands.UpdateTrapCard
{
    public class UpdateTrapCardCommandValidator : AbstractValidator<UpdateTrapCardCommand>
    {
        public UpdateTrapCardCommandValidator()
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .CardNameValidator();
        }
    }
}