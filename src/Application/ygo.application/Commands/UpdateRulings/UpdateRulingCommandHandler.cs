using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.Commands.UpdateRulings
{
    public class UpdateRulingCommandHandler : IRequestHandler<UpdateRulingCommand, CommandResult>
    {
        private readonly ICardRulingRepository _cardRulingRepository;
        private readonly IValidator<UpdateRulingCommand> _validator;

        public UpdateRulingCommandHandler(ICardRulingRepository cardRulingRepository, IValidator<UpdateRulingCommand> validator)
        {
            _cardRulingRepository = cardRulingRepository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateRulingCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validatorResults = _validator.Validate(request);

            if (validatorResults.IsValid)
            {
                await _cardRulingRepository.DeleteByCardId(request.CardId);

                var newRulingSectionList = new List<RulingSection>();

                foreach (var rulingSectionDto in request.Rulings)
                {
                    var newRulingSection = new RulingSection
                    {
                        CardId = request.CardId,
                        Name = rulingSectionDto.Name,
                        Created = DateTime.UtcNow,
                        Updated = DateTime.UtcNow
                    };

                    foreach (var ruling in rulingSectionDto.Rulings)
                    {
                        newRulingSection.Ruling.Add(new Ruling
                        {
                            RulingSection = newRulingSection,
                            Text = ruling,
                            Created = DateTime.UtcNow,
                            Updated = DateTime.UtcNow
                        });
                    }

                    newRulingSectionList.Add(newRulingSection);
                }

                if (newRulingSectionList.Any())
                {
                    await _cardRulingRepository.Update(newRulingSectionList);
                    
                    commandResult.IsSuccessful = true;
                }
            }

            return commandResult;
        }
    }
}