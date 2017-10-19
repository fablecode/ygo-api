using AutoMapper;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.Commands.AddBanlist
{
    public class AddBanlistCommandHandler : IAsyncRequestHandler<AddBanlistCommand, CommandResult>
    {
        private readonly IBanlistRepository _banlistRepository;
        private readonly IValidator<AddBanlistCommand> _validator;

        public AddBanlistCommandHandler(IBanlistRepository banlistRepository, IValidator<AddBanlistCommand> validator)
        {
            _banlistRepository = banlistRepository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(AddBanlistCommand message)
        {
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
            {
                var newBanlist = Mapper.Map<Banlist>(message);

                var newBanlistResult = await _banlistRepository.Add(newBanlist);
                commandResult.Data = newBanlistResult.Id;
                commandResult.IsSuccessful = true;
            }
            else
            {
                commandResult.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }
    }
}