using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.domain.Repository;

namespace ygo.application.Commands.AddTrapCard
{
    public class AddTrapCardCommandHandler : IAsyncRequestHandler<AddTrapCardCommand, CommandResult>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<AddTrapCardCommand> _validator;

        public AddTrapCardCommandHandler(ICardRepository repository, IValidator<AddTrapCardCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(AddTrapCardCommand message)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(message);

            if (validationResult.IsValid)
            {
                var newTrapCard = message.MapToCard();

                var newTrapCardResult = await _repository.Add(newTrapCard);

                commandResult.Data = newTrapCardResult.Id;
                commandResult.IsSuccessful = true;
            }
            else
            {
                commandResult.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }
    }
}