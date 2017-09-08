using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.application.Repository;

namespace ygo.application.Commands.AddSpellCard
{
    public class AddSpellCardCommandHandler : IAsyncRequestHandler<AddSpellCardCommand, CommandResult>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<AddSpellCardCommand> _validator;

        public AddSpellCardCommandHandler(ICardRepository repository, IValidator<AddSpellCardCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(AddSpellCardCommand message)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(message);

            if (validationResult.IsValid)
            {
                var newspellCard = message.MapToCard();

                commandResult.Data = await _repository.Add(newspellCard);
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