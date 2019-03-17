using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.DownloadImage;
using ygo.application.Configuration;
using ygo.core.Models.Db;
using ygo.core.Services;

namespace ygo.application.Commands.AddArchetype
{
    public class AddArchetypeCommandHandler : IRequestHandler<AddArchetypeCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddArchetypeCommand> _validator;
        private readonly IArchetypeService _archetypeService;
        private readonly IOptions<ApplicationSettings> _settings;

        public AddArchetypeCommandHandler
        (
            IMediator mediator, 
            IValidator<AddArchetypeCommand> validator, 
            IArchetypeService archetypeService, 
            IOptions<ApplicationSettings> settings
        )
        {
            _mediator = mediator;
            _validator = validator;
            _archetypeService = archetypeService;
            _settings = settings;
        }

        public async Task<CommandResult> Handle(AddArchetypeCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid)
            {
                var newArchetype = await _archetypeService.Add(new Archetype
                {
                    Id = request.ArchetypeNumber,
                    Name = request.Name,
                    Url = request.ProfileUrl,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                });

                if (!string.IsNullOrWhiteSpace(request.ImageUrl))
                {
                    var downloadImageCommand = new DownloadImageCommand
                    {
                        RemoteImageUrl = new Uri(request.ImageUrl),
                        ImageFileName = newArchetype.Id.ToString(),
                        ImageFolderPath = _settings.Value.ArchetypeImageFolderPath
                    };

                    await _mediator.Send(downloadImageCommand, cancellationToken);
                }

                commandResult.Data = newArchetype.Id;
                commandResult.IsSuccessful = true;
            }
            else
            {
                commandResult.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }
    }
}