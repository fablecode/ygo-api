using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.DownloadImage;
using ygo.application.Dto;
using ygo.application.Enums;
using ygo.application.Models.Cards.Input;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Helpers;

namespace ygo.application.Commands.UpdateCard
{
    public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CardInputModel> _validator;
        private readonly ICardService _cardService;
        private readonly IOptions<ApplicationSettings> _settings;

        public UpdateCardCommandHandler
        (
            IMediator mediator, 
            IValidator<CardInputModel> validator,
            ICardService cardService,
            IOptions<ApplicationSettings> settings
        )
        {
            _mediator = mediator;
            _validator = validator;
            _cardService = cardService;
            _settings = settings;
        }

        public async Task<CommandResult> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(request.Card);

            if (validationResults.IsValid)
            {
                var cardModel = Mapper.Map<CardModel>(request.Card);

                var cardUpdated = await _cardService.Update(cardModel);

                if (cardUpdated != null)
                {
                    if (request.Card.ImageUrl != null)
                    {
                        var downloadImageCommand = new DownloadImageCommand
                        {
                            RemoteImageUrl = request.Card.ImageUrl,
                            ImageFileName = request.Card.Name.MakeValidFileName(),
                            ImageFolderPath = _settings.Value.CardImageFolderPath
                        };

                        await _mediator.Send(downloadImageCommand, cancellationToken);
                    }

                    commandResult.Data = MapCardByCardType(request.Card.CardType.GetValueOrDefault(), cardUpdated);
                    commandResult.IsSuccessful = true;
                }
                else
                {
                    commandResult.Errors = new List<string> { "Card not updated in data source." };
                }
            }
            else
            {
                commandResult.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }

        private object MapCardByCardType(YgoCardType cardCardType, Card cardUpdated)
        {
            switch (cardCardType)
            {
                case YgoCardType.Monster:
                    return Mapper.Map<MonsterCardDto>(cardUpdated);
                case YgoCardType.Spell:
                    return Mapper.Map<SpellCardDto>(cardUpdated);
                case YgoCardType.Trap:
                    return Mapper.Map<TrapCardDto>(cardUpdated);
                default:
                    throw new ArgumentOutOfRangeException(nameof(cardCardType), cardCardType, null);
            }
        }
    }
}