using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.DownloadImage;
using ygo.application.Commands.UpdateMonsterCard;
using ygo.application.Commands.UpdateSpellCard;
using ygo.application.Commands.UpdateTrapCard;
using ygo.core.Enums;
using ygo.domain.Helpers;

namespace ygo.application.Commands.UpdateCard
{
    public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<UpdateCardCommand> _validator;

        public UpdateCardCommandHandler(IMediator mediator, IValidator<UpdateCardCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                CommandResult cardTypeCommandResult;

                switch (request.CardType)
                {
                    case YgoCardType.Monster:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<UpdateMonsterCardCommand>(request));
                        break;
                    case YgoCardType.Spell:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<UpdateSpellCardCommand>(request));
                        break;
                    case YgoCardType.Trap:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<UpdateTrapCardCommand>(request));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(request.CardType));
                }

                if (cardTypeCommandResult.IsSuccessful)
                {
                    if (request.ImageUrl != null)
                    {
                        var downloadImageCommand = new DownloadImageCommand
                        {
                            RemoteImageUrl = request.ImageUrl,
                            ImageFileName = request.Name.MakeValidFileName(),
                        };

                        await _mediator.Send(downloadImageCommand, cancellationToken);
                    }
                }

                commandResult = cardTypeCommandResult;
            }
            else
            {
                commandResult.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }
    }
}