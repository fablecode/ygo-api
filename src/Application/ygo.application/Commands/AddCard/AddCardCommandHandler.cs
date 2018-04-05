using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.AddMonsterCard;
using ygo.application.Commands.AddSpellCard;
using ygo.application.Commands.AddTrapCard;
using ygo.application.Commands.DownloadImage;
using ygo.core.Enums;
using ygo.domain.Helpers;

namespace ygo.application.Commands.AddCard
{
    public class AddCardCommandHandler : IRequestHandler<AddCardCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddCardCommand> _validator;
        private readonly IOptions<ApplicationSettings> _settings;

        public AddCardCommandHandler(IMediator mediator, IValidator<AddCardCommand> validator, IOptions<ApplicationSettings> settings)
        {
            _mediator = mediator;
            _validator = validator;
            _settings = settings;
        }

        public async Task<CommandResult> Handle(AddCardCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                CommandResult cardTypeCommandResult;

                switch (request.CardType)
                {
                    case YgoCardType.Monster:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<AddMonsterCardCommand>(request), cancellationToken);
                        break;
                    case YgoCardType.Spell:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<AddSpellCardCommand>(request), cancellationToken);
                        break;
                    case YgoCardType.Trap:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<AddTrapCardCommand>(request), cancellationToken);
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