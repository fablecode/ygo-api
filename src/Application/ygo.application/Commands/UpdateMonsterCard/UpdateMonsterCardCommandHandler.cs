﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.application.Repository;

namespace ygo.application.Commands.UpdateMonsterCard
{
    public class UpdateMonsterCardCommandHandler : IAsyncRequestHandler<UpdateMonsterCardCommand, CommandResult>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<UpdateMonsterCardCommand> _validator;

        public UpdateMonsterCardCommandHandler(ICardRepository repository, IValidator<UpdateMonsterCardCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateMonsterCardCommand message)
        {
            var commandResult = new CommandResult();

            var validateResults = _validator.Validate(message);

            if (validateResults.IsValid)
            {
                var cardToUpdate = await _repository.CardById(message.Id);

                if (cardToUpdate != null)
                {
                    cardToUpdate.UpdateMonsterCardWith(message);

                    commandResult.Data = await _repository.Update(cardToUpdate);
                    commandResult.IsSuccessful = true;
                }
                else
                {
                    commandResult.Errors = new List<string>{"Critical error: Card not found."};
                }
            }
            else
                commandResult.Errors = validateResults.Errors.Select(err => err.ErrorMessage).ToList();

            return commandResult;
        }
    }
}