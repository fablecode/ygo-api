using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;

namespace ygo.application.Commands.UpdateTips
{
    public class UpdateTipsCommandHandler : IRequestHandler<UpdateTipsCommand, CommandResult>
    {
        private readonly ICardTipService _cardTipService;
        private readonly IValidator<UpdateTipsCommand> _validator;

        public UpdateTipsCommandHandler(ICardTipService cardTipService, IValidator<UpdateTipsCommand> validator)
        {
            _cardTipService = cardTipService;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateTipsCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validatorResults = _validator.Validate(request);

            if (validatorResults.IsValid)
            {
                await _cardTipService.DeleteByCardId(request.CardId);

                var newTipSectionList = new List<TipSection>();

                foreach (var tipSectionDto in request.Tips)
                {
                    var newTipSection = new TipSection
                    {
                        CardId = request.CardId,
                        Name = tipSectionDto.Name,
                        Created = DateTime.UtcNow,
                        Updated = DateTime.UtcNow
                    };

                    foreach (var tip in tipSectionDto.Tips)
                    {
                        newTipSection.Tip.Add(new Tip
                        {
                            TipSection = newTipSection,
                            Text = tip,
                            Created = DateTime.UtcNow,
                            Updated = DateTime.UtcNow
                        });
                    }

                    newTipSectionList.Add(newTipSection);
                }

                if (newTipSectionList.Any())
                {
                    await _cardTipService.Update(newTipSectionList);
                    
                    commandResult.IsSuccessful = true;
                }
            }

            return commandResult;
        }
    }
}