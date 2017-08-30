using FluentValidation;

namespace ygo.application.Queries.CardByName
{
    public class CardByNameQueryValidator : AbstractValidator<CardByNameQuery>
    {
        public CardByNameQueryValidator()
        {
            RuleFor(q => q.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .Length(2, 255);
        }
    }
}