using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ygo.application.Dto;
using ygo.application.Repository;

namespace ygo.application.Commands.UpdateSpellCard
{
    public class UpdateSpellCardCommandHandler : IAsyncRequestHandler<UpdateSpellCardCommand, CommandResult>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<UpdateSpellCardCommand> _validator;

        public UpdateSpellCardCommandHandler(ICardRepository repository, IValidator<UpdateSpellCardCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateSpellCardCommand message)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(message);

            if (validationResult.IsValid)
            {
                var cardToUpdate = await _repository.CardById(message.Id);
                if (cardToUpdate != null)
                {
                    cardToUpdate.UpdateSpellCardWith(message);

                    commandResult.Data = Mapper.Map<SpellCardDto>(await _repository.Update(cardToUpdate));
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