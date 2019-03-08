using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;

namespace ygo.application.Commands.UpdateRulings
{
    public class UpdateRulingCommandHandler : IRequestHandler<UpdateRulingCommand, CommandResult>
    {
        private readonly ICardRulingService _cardRulingService;
        private readonly IValidator<UpdateRulingCommand> _validator;

        public UpdateRulingCommandHandler(ICardRulingService cardRulingService, IValidator<UpdateRulingCommand> validator)
        {
            _cardRulingService = cardRulingService;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateRulingCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validatorResults = _validator.Validate(request);

            if (validatorResults.IsValid)
            {
                await _cardRulingService.DeleteByCardId(request.CardId);

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
                    await _cardRulingService.Update(newRulingSectionList);
                    
                    commandResult.IsSuccessful = true;
                }
            }

            return commandResult;
        }
    }
}