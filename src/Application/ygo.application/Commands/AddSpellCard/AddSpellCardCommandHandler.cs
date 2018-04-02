using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.domain.Repository;

namespace ygo.application.Commands.AddSpellCard
{
    public class AddSpellCardCommandHandler : IRequestHandler<AddSpellCardCommand, CommandResult>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<AddSpellCardCommand> _validator;

        public AddSpellCardCommandHandler(ICardRepository repository, IValidator<AddSpellCardCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(AddSpellCardCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid)
            {
                var newSpellCard = request.MapToCard();

                var newSpellCardResult = await _repository.Add(newSpellCard);
                commandResult.Data = newSpellCardResult.Id;
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