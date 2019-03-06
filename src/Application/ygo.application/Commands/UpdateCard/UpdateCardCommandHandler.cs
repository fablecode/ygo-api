using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ygo.application.Commands.DownloadImage;
using ygo.application.Commands.UpdateMonsterCard;
using ygo.application.Commands.UpdateSpellCard;
using ygo.application.Commands.UpdateTrapCard;
using ygo.application.Enums;
using ygo.core.Enums;
using ygo.domain.Helpers;

namespace ygo.application.Commands.UpdateCard
{
    public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<UpdateCardCommand> _validator;
        private readonly IOptions<ApplicationSettings> _settings;

        public UpdateCardCommandHandler(IMediator mediator, IValidator<UpdateCardCommand> validator, IOptions<ApplicationSettings> settings)
        {
            _mediator = mediator;
            _validator = validator;
            _settings = settings;
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
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<UpdateMonsterCardCommand>(request), cancellationToken);
                        break;
                    case YgoCardType.Spell:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<UpdateSpellCardCommand>(request), cancellationToken);
                        break;
                    case YgoCardType.Trap:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<UpdateTrapCardCommand>(request), cancellationToken);
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
                            ImageFolderPath = _settings.Value.CardImageFolderPath
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