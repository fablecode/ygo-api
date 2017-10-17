using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ygo.application.Dto;
using ygo.domain.Repository;

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
                var cardToUpdate = await _repository.CardById(message.Id);
                if (cardToUpdate != null)
                {
                    cardToUpdate.UpdateTrapCardWith(message);

                    commandResult.Data = Mapper.Map<TrapCardDto>(await _repository.Update(cardToUpdate));
                    commandResult.IsSuccessful = true;
                }
                else
                {
                    commandResult.Errors = new List<string> { "Critical error: Card not found." };
                }
            }
            else
            {
                commandResult.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }
    }
}