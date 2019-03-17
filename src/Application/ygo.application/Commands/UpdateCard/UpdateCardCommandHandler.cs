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
using ygo.application.Configuration;
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
        private readonly IMapper _mapper;

        public UpdateCardCommandHandler
        (
            IMediator mediator, 
            IValidator<CardInputModel> validator,
            ICardService cardService,
            IOptions<ApplicationSettings> settings,
            IMapper mapper
        )
        {
            _mediator = mediator;
            _validator = validator;
            _cardService = cardService;
            _settings = settings;
            _mapper = mapper;
        }

        public async Task<CommandResult> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(request.Card);

            if (validationResults.IsValid)
            {
                var cardModel = _mapper.Map<CardModel>(request.Card);

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

                    commandResult.Data = CommandMapperHelper.MapCardByCardType(request.Card.CardType.GetValueOrDefault(), cardUpdated);
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

    }
}