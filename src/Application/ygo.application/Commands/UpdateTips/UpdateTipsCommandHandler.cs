using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.Commands.UpdateTips
{
    public class UpdateTipsCommandHandler : IRequestHandler<UpdateTipsCommand, CommandResult>
    {
        private readonly ICardTipRepository _cardTipRepository;
        private readonly IValidator<UpdateTipsCommand> _validator;

        public UpdateTipsCommandHandler(ICardTipRepository cardTipRepository, IValidator<UpdateTipsCommand> validator)
        {
            _cardTipRepository = cardTipRepository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateTipsCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validatorResults = _validator.Validate(request);

            if (validatorResults.IsValid)
            {
                //var card = await _cardRepository.CardById(request.CardId);
                await _cardTipRepository.DeleteByCardId(request.CardId);

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
                    await _cardTipRepository.Update(newTipSectionList);
                    commandResult.IsSuccessful = true;
                }
            }

            return commandResult;
        }
    }
}