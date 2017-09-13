using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ygo.application.Ioc;
using ygo.application.Repository;

namespace ygo.application.Commands.AddMonsterCard
{
    public class AddMonsterCardCommandHandler : IAsyncRequestHandler<AddMonsterCardCommand, CommandResult>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<AddMonsterCardCommand> _validator;

        public AddMonsterCardCommandHandler(ICardRepository repository, IValidator<AddMonsterCardCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(AddMonsterCardCommand message)
        {
            var commandResult = new CommandResult();

            var validateResults = _validator.Validate(message);

            if (validateResults.IsValid)
            {
                var newMonsterCard = message.MapToCard();

                var newMonsterCardResult = await _repository.Add(newMonsterCard);

                commandResult.Data = newMonsterCardResult.Id;
                commandResult.IsSuccessful = true;
            }
            else
                commandResult.Errors = validateResults.Errors.Select(err => err.ErrorMessage).ToList();

            return commandResult;
        }
    }
}