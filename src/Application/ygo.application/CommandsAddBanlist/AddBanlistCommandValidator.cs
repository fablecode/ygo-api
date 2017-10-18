using FluentValidation;

namespace ygo.application.CommandsAddBanlist
{
    public class AddBanlistCommandValidator : AbstractValidator<AddBanlistCommand>
    {
        public AddBanlistCommandValidator()
        {
            RuleFor(b => b.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .GreaterThan(0);

            RuleFor(b => b.FormatId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .GreaterThan(0);

            RuleFor(b => b.Name)
                .NotNull()
                .NotEmpty()
                .Length(1, 255);
        }
    }
}