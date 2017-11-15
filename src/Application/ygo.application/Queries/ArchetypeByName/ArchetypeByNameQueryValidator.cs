using FluentValidation;

namespace ygo.application.Queries.ArchetypeByName
{
    public class ArchetypeByNameQueryValidator : AbstractValidator<ArchetypeByNameQuery>
    {
        public ArchetypeByNameQueryValidator()
        {
            RuleFor(a => a.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();
        }
    }
}