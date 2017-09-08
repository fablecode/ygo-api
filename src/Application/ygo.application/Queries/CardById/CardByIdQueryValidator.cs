using FluentValidation;

namespace ygo.application.Queries.CardById
{
    public class CardByIdQueryValidator : AbstractValidator<CardByIdQuery>
    {
        public CardByIdQueryValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}