using FluentValidation;

namespace ygo.application.Commands.UpdateBanlistCards
{
    public class UpdateBanlistCardsCommandValidator : AbstractValidator<UpdateBanlistCardsCommand>
    {
        public UpdateBanlistCardsCommandValidator()
        {
            RuleFor(bc => bc.BanlistCards)
                .NotNull()
                .NotEmpty();
        }
    }
}