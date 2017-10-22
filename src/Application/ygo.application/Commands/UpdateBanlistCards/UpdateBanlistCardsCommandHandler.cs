using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ygo.application.Dto;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.Commands.UpdateBanlistCards
{
    public class UpdateBanlistCardsCommandHandler : IAsyncRequestHandler<UpdateBanlistCardsCommand, CommandResult>
    {
        private readonly IBanlistCardsRepository _banlistCardsRepository;
        private readonly IValidator<UpdateBanlistCardsCommand> _validator;

        public UpdateBanlistCardsCommandHandler(IBanlistCardsRepository banlistCardsRepository, IValidator<UpdateBanlistCardsCommand> validator)
        {
            _banlistCardsRepository = banlistCardsRepository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateBanlistCardsCommand message)
        {
            var commandResult = new CommandResult();

            var validatorResults = _validator.Validate(message);

            if (validatorResults.IsValid)
            {
                var banlistCards = message
                                    .BanlistCards
                                    .Select( bl => new BanlistCard { BanlistId = bl.BanlistId, CardId = bl.CardId, LimitId = bl.LimitId})
                                    .ToArray();

                await _banlistCardsRepository.Update(message.BanlistId, banlistCards);

                commandResult.Data = message.BanlistCards;
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