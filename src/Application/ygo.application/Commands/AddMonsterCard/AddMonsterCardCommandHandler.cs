using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.domain.Repository;

namespace ygo.application.Commands.AddMonsterCard
{
    public class AddMonsterCardCommandHandler : IRequestHandler<AddMonsterCardCommand, CommandResult>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<AddMonsterCardCommand> _validator;

        public AddMonsterCardCommandHandler(ICardRepository repository, IValidator<AddMonsterCardCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(AddMonsterCardCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validateResults = _validator.Validate(request);

            if (validateResults.IsValid)
            {
                var newMonsterCard = request.MapToCard();

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