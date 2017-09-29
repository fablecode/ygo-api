using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using ygo.application.Commands.DownloadImage;
using ygo.application.Commands.UpdateMonsterCard;
using ygo.application.Commands.UpdateSpellCard;
using ygo.application.Commands.UpdateTrapCard;
using ygo.application.Enums;
using ygo.application.Helpers;

namespace ygo.application.Commands.UpdateCard
{
    public class UpdateCardCommandHandler : IAsyncRequestHandler<UpdateCardCommand, CommandResult>
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

        public async Task<CommandResult> Handle(UpdateCardCommand message)
        {
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
            {
                CommandResult cardTypeCommandResult;

                switch (message.CardType)
                {
                    case YgoCardType.Monster:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<UpdateMonsterCardCommand>(message));
                        break;
                    case YgoCardType.Spell:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<UpdateSpellCardCommand>(message));
                        break;
                    case YgoCardType.Trap:
                        cardTypeCommandResult = await _mediator.Send(Mapper.Map<UpdateTrapCardCommand>(message));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(message.CardType));
                }

                if (cardTypeCommandResult.IsSuccessful)
                {
                    if (message.ImageUrl != null)
                    {
                        var downloadImageCommand = new DownloadImageCommand
                        {
                            RemoteImageUrl = message.ImageUrl,
                            ImageFileName = message.Name.MakeValidFileName(),
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