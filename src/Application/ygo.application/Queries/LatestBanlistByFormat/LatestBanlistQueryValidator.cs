using FluentValidation;

namespace ygo.application.Queries.LatestBanlistByFormat
{
    public class LatestBanlistQueryValidator : AbstractValidator<LatestBanlistQuery>
    {
        public LatestBanlistQueryValidator()
        {
            RuleFor(lb => lb.Acronym)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();
        }
    }
}