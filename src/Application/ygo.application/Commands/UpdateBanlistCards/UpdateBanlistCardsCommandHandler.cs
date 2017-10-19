using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
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
                commandResult.Data = await _banlistCardsRepository.Update(message.BanlistId, Mapper.Map<BanlistCard[]>(message.BanlistCards));
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