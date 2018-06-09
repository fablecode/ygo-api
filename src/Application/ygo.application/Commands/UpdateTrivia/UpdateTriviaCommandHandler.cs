using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.application.Commands.UpdateTips;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.Commands.UpdateTrivia
{
    public class UpdateTriviaCommandHandler : IRequestHandler<UpdateTriviaCommand, CommandResult>
    {
        private readonly ICardTriviaRepository _cardTriviaRepository;
        private readonly IValidator<UpdateTriviaCommand> _validator;

        public UpdateTriviaCommandHandler(ICardTriviaRepository cardTriviaRepository, IValidator<UpdateTriviaCommand> validator)
        {
            _cardTriviaRepository = cardTriviaRepository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateTriviaCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validatorResults = _validator.Validate(request);

            if (validatorResults.IsValid)
            {
                await _cardTriviaRepository.DeleteByCardId(request.CardId);

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
                    await _cardTriviaRepository.Update(newTriviaSectionList);
                    
                    commandResult.IsSuccessful = true;
                }
            }

            return commandResult;
        }
    }
}