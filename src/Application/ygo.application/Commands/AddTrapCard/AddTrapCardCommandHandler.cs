using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.domain.Repository;

namespace ygo.application.Commands.AddTrapCard
{
    public class AddTrapCardCommandHandler : IRequestHandler<AddTrapCardCommand, CommandResult>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<AddTrapCardCommand> _validator;

        public AddTrapCardCommandHandler(ICardRepository repository, IValidator<AddTrapCardCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(AddTrapCardCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid)
            {
                var newTrapCard = request.MapToCard();

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