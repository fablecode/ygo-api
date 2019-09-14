using FluentValidation;

namespace ygo.application.Queries.CardSearch
{
    public class CardSearchQueryValidator : AbstractValidator<CardSearchQuery>
    {
        public CardSearchQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(a => a.PageIndex)
                .GreaterThan(0);

            RuleFor(a => a.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(10);

        }
    }
}