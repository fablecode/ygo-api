using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;

namespace ygo.application.Commands.UpdateBanlistCards
{
    public class UpdateBanlistCardsCommandHandler : IRequestHandler<UpdateBanlistCardsCommand, CommandResult>
    {
        private readonly IBanlistCardsService _banlistCardsService;
        private readonly IValidator<UpdateBanlistCardsCommand> _validator;

        public UpdateBanlistCardsCommandHandler(IBanlistCardsService banlistCardsService, IValidator<UpdateBanlistCardsCommand> validator)
        {
            _banlistCardsService = banlistCardsService;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateBanlistCardsCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validatorResults = _validator.Validate(request);

            if (validatorResults.IsValid)
            {
                var banlistCards = request
                    .BanlistCards
                    .Select(bl => new BanlistCard { BanlistId = bl.BanlistId, CardId = bl.CardId, LimitId = bl.LimitId })
                    .ToArray();

                await _banlistCardsService.Update(request.BanlistId, banlistCards);

                commandResult.Data = request.BanlistCards;
                commandResult.IsSuccessful = true;
            }
            else
            {
                commandResult.Errors = validatorResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }
    }
}