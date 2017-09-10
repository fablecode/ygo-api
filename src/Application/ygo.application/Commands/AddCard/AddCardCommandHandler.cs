﻿using System;
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
using ygo.domain.Models;

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
                    var insertedCard = (Card)cardTypeCommandResult.Data;

                    var imageFileName = insertedCard.Id.ToString();
                    var cardLocalImageFileName = Path.Combine(_settings.Value.CardImageFolderPath, imageFileName);
                    var downloadedImageFile = await _mediator.Send(new DownloadImageCommand { FileName = imageFileName, RemoteImageUrl = message.ImageUrl, LocalImageFileName = cardLocalImageFileName });
                }
            }
            else
            {
                commandResult.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }
    }
}