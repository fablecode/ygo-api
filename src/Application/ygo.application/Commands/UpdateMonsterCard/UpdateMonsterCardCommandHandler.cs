using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Commands.UpdateMonsterCard
{
    public class UpdateMonsterCardCommandHandler : IRequestHandler<UpdateMonsterCardCommand, CommandResult>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<UpdateMonsterCardCommand> _validator;

        public UpdateMonsterCardCommandHandler(ICardRepository repository, IValidator<UpdateMonsterCardCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateMonsterCardCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validateResults = _validator.Validate(request);

            if (validateResults.IsValid)
            {
                var cardToUpdate = await _repository.CardById(request.Id);

                if (cardToUpdate != null)
                {
                    cardToUpdate.UpdateMonsterCardWith(request);

                    commandResult.Data = Mapper.Map<MonsterCardDto>(await _repository.Update(cardToUpdate));
                    commandResult.IsSuccessful = true;
                }
                else
                {
                    commandResult.Errors = new List<string> { "Critical error: Card not found." };
                }
            }
            else
                commandResult.Errors = validateResults.Errors.Select(err => err.ErrorMessage).ToList();

            return commandResult;
        }
    }
}