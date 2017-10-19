using FluentValidation;

namespace ygo.application.Commands.AddBanlist
{
    public class AddBanlistCommandValidator : AbstractValidator<AddBanlistCommand>
    {
        public AddBanlistCommandValidator()
        {
            RuleFor(b => b.Id)
                .GreaterThan(0);

            RuleFor(b => b.FormatId)
                .GreaterThan(0);

            RuleFor(b => b.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .Length(1, 255);
        }
    }
}