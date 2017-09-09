using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.application.Repository;

namespace ygo.application.Commands.UpdateTrapCard
{
    public class UpdateTrapCardCommandHandler : IAsyncRequestHandler<UpdateTrapCardCommand, CommandResult>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<UpdateTrapCardCommand> _validator;

        public UpdateTrapCardCommandHandler(ICardRepository repository, IValidator<UpdateTrapCardCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateTrapCardCommand message)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(message);

            if (validationResult.IsValid)
            {
                var updateTrapCard = message.MapToCard();

                commandResult.Data = await _repository.Update(updateTrapCard);
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