using FluentValidation;

namespace ygo.application.Queries.FormatByAcronym
{
    public class FormatByAcronymQueryValidator : AbstractValidator<FormatByAcronymQuery>
    {
        public FormatByAcronymQueryValidator()
        {
            RuleFor(f => f.Acronym)
                .NotNull()
                .NotEmpty()
                .Length(3);
        }
    }
}