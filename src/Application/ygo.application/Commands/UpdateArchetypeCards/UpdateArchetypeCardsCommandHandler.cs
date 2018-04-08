﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.application.Commands.UpdateArchetype;
using ygo.domain.Repository;

namespace ygo.application.Commands.UpdateArchetypeCards
{
    public class UpdateArchetypeCardsCommandHandler : IRequestHandler<UpdateArchetypeCommand, CommandResult>
    {
        private readonly IValidator<UpdateArchetypeCardsCommand> _validator;
        private readonly IArchetypeCardsRepository _archetypeCardsRepository;

        public UpdateArchetypeCardsCommandHandler(IValidator<UpdateArchetypeCardsCommand> validator, IArchetypeCardsRepository archetypeCardsRepository)
        {
            _validator = validator;
            _archetypeCardsRepository = archetypeCardsRepository;
        }
        public async Task<CommandResult> Handle(UpdateArchetypeCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid)
            {
                if (request.Cards.Any())
                {
                    var result = await _archetypeCardsRepository.Update(request.Id, request.Cards);

                    commandResult.Data = result;
                }

                commandResult.IsSuccessful = true;
            }

            return commandResult;
        }
    }
}