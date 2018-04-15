using FluentValidation;

namespace ygo.application.Queries.ArchetypeSearch
{
    public class ArchetypeSearchQueryValidator : AbstractValidator<ArchetypeSearchQuery>
    {
        public ArchetypeSearchQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(a => a.PageNumber)
                .GreaterThan(0);

            RuleFor(a => a.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(10);

        }
    }
}