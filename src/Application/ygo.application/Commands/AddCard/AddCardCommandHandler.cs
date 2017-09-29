using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using ygo.application.Commands.AddMonsterCard;
using ygo.application.Commands.AddSpellCard;
using ygo.application.Commands.AddTrapCard;
using ygo.application.Commands.DownloadImage;
using ygo.application.Enums;
using ygo.application.Helpers;

namespace ygo.application.Commands.AddCard
{
    public class AddCardCommandHandler : IAsyncRequestHandler<AddCardCommand, CommandResult>
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

        public async Task<CommandResult> Handle(AddCardCommand message)
        {
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
            {
                CommandResult cardTypeCommandResult;

                switch (message.CardType)
                {
                    case YgoCardType.Monster:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<AddMonsterCardCommand>(message));
                        break;
                    case YgoCardType.Spell:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<AddSpellCardCommand>(message));
                        break;
                    case YgoCardType.Trap:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<AddTrapCardCommand>(message));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(message.CardType));
                }

                if (cardTypeCommandResult.IsSuccessful)
                {
                    if (message.ImageUrl != null)
                    {
                        var localFileNameExtension = Path.GetExtension(message.ImageUrl.AbsolutePath);
                        var localFileName = string.Concat(message.Name.MakeValidFileName(), localFileNameExtension);

                        var imageFileNameFullPath = Path.Combine(_settings.Value.CardImageFolderPath, localFileName);

                        var downloadImageCommand = new DownloadImageCommand
                        {
                            RemoteImageUrl = message.ImageUrl,
                            ImageFileName = imageFileNameFullPath,
                        };

                        await _mediator.Send(downloadImageCommand);
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