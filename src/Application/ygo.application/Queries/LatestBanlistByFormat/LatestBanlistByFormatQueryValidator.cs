using FluentValidation;

namespace ygo.application.Queries.LatestBanlistByFormat
{
    public class LatestBanlistByFormatQueryValidator : AbstractValidator<LatestBanlistByFormatQuery>
    {
        public LatestBanlistByFormatQueryValidator()
        {
            DefaultValidatorOptions.Cascade(RuleFor(lb => lb.Format), CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();
        }
    }
}