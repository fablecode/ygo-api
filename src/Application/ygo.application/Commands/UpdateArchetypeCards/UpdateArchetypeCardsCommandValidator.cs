using FluentValidation;

namespace ygo.application.Commands.UpdateArchetypeCards
{
    public class UpdateArchetypeCardsCommandValidator : AbstractValidator<UpdateArchetypeCardsCommand>
    {
        public UpdateArchetypeCardsCommandValidator()
        {
            RuleFor(a => a.ArchetypeId)
                .GreaterThan(0);
        }
    }
}