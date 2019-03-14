using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;

namespace ygo.application.Commands.UpdateTrivia
{
    public class UpdateTriviaCommandHandler : IRequestHandler<UpdateTriviaCommand, CommandResult>
    {
        private readonly ICardTriviaService _cardTriviaService;
        private readonly IValidator<UpdateTriviaCommand> _validator;

        public UpdateTriviaCommandHandler(ICardTriviaService cardTriviaService, IValidator<UpdateTriviaCommand> validator)
        {
            _cardTriviaService = cardTriviaService;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateTriviaCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validatorResults = _validator.Validate(request);

            if (validatorResults.IsValid)
            {
                await _cardTriviaService.DeleteByCardId(request.CardId);

                var newTriviaSectionList = new List<TriviaSection>();

                foreach (var triviaSectionDto in request.Trivia)
                {
                    var newTriviaSection = new TriviaSection
                    {
                        CardId = request.CardId,
                        Name = triviaSectionDto.Name,
                        Created = DateTime.UtcNow,
                        Updated = DateTime.UtcNow
                    };

                    foreach (var trivia in triviaSectionDto.Trivia)
                    {
                        newTriviaSection.Trivia.Add(new Trivia
                        {
                            TriviaSection = newTriviaSection,
                            Text = trivia,
                            Created = DateTime.UtcNow,
                            Updated = DateTime.UtcNow
                        });
                    }

                    newTriviaSectionList.Add(newTriviaSection);
                }

                if (newTriviaSectionList.Any())
                {
                    await _cardTriviaService.Update(newTriviaSectionList);
                    
                    commandResult.IsSuccessful = true;
                }
            }
            else
            {
                commandResult.Errors = validatorResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }
    }
}