using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.DownloadImage;
using ygo.application.Configuration;
using ygo.application.Models.Cards.Input;
using ygo.core.Models;
using ygo.core.Services;
using ygo.domain.Helpers;

namespace ygo.application.Commands.AddCard
{
    public class AddCardCommandHandler : IRequestHandler<AddCardCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CardInputModel> _validator;
        private readonly ICardService _cardService;
        private readonly IOptions<ApplicationSettings> _settings;
        private readonly IMapper _mapper;

        public AddCardCommandHandler
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

        public async Task<CommandResult> Handle(AddCardCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            if (request.Card != null)
            {
                var validationResults = _validator.Validate(request.Card);

                if (validationResults.IsValid)
                {
                    var cardModel = _mapper.Map<CardModel>(request.Card);

                    var result = await _cardService.Add(cardModel);

                    if (result != null)
                    {
                        if (request.Card.ImageUrl != null)
                        {
                            var downloadImageCommand = new DownloadImageCommand
                            {
                                RemoteImageUrl = request.Card.ImageUrl,
                                ImageFileName = request.Card.Name.SanitizeFileName(),
                                ImageFolderPath = _settings.Value.CardImageFolderPath
                            };

                            await _mediator.Send(downloadImageCommand, cancellationToken);
                        }

                        commandResult.Data = result.Id;
                        commandResult.IsSuccessful = true;
                    }
                    else
                    {
                        commandResult.Errors = new List<string>{ "Card not persisted to data source."};
                    }
                }
                else
                {
                    commandResult.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
                }
            }
            else
            {
                commandResult.Errors = new List<string>{ "Card must not be null or empty"};
            }

            return commandResult;
        }
    }
}