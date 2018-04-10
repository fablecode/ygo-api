using FluentValidation;

namespace ygo.application.Commands.UpdateArchetypeSupportCards
{
    public class UpdateArchetypeSupportCardsCommandValidator : AbstractValidator<UpdateArchetypeSupportCardsCommand>
    {
        public UpdateArchetypeSupportCardsCommandValidator()
        {
            RuleFor(a => a.ArchetypeId)
                .GreaterThan(0);
        }
    }
}