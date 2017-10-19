using FluentValidation;

namespace ygo.application.Commands.UpdateBanlist
{
    public class UpdateBanlistCommandValidator : AbstractValidator<UpdateBanlistCommand>
    {
        public UpdateBanlistCommandValidator()
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

            RuleFor(b => b.ReleaseDate)
                .NotNull();
        }
    }
}