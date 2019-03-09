using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.DownloadImage;
using ygo.core.Services;

namespace ygo.application.Commands.UpdateArchetype
{
    public class UpdateArchetypeCommandHandler : IRequestHandler<UpdateArchetypeCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<UpdateArchetypeCommand> _validator;
        private readonly IArchetypeService _archetypeService;
        private readonly IOptions<ApplicationSettings> _settings;

        public UpdateArchetypeCommandHandler
        (
            IMediator mediator,
            IValidator<UpdateArchetypeCommand> validator,
            IArchetypeService archetypeService,
            IOptions<ApplicationSettings> settings
        )
        {
            _mediator = mediator;
            _validator = validator;
            _archetypeService = archetypeService;
            _settings = settings;
        }

        public async Task<CommandResult> Handle(UpdateArchetypeCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid)
            {
                var archetypeToUpdate = await _archetypeService.ArchetypeById(request.Id);

                if (archetypeToUpdate != null)
                {
                    archetypeToUpdate.Name = request.Name;
                    archetypeToUpdate.Url = request.ProfileUrl;
                    archetypeToUpdate.Updated = DateTime.UtcNow;

                    commandResult.Data = await _archetypeService.Update(archetypeToUpdate);

                    if (!string.IsNullOrWhiteSpace(request.ImageUrl))
                    {
                        var downloadImageCommand = new DownloadImageCommand
                        {
                            RemoteImageUrl = new Uri(request.ImageUrl),
                            ImageFileName = request.Id.ToString(),
                            ImageFolderPath = _settings.Value.ArchetypeImageFolderPath
                        };

                        await _mediator.Send(downloadImageCommand, cancellationToken);
                    }

                    commandResult.IsSuccessful = true;
                }
                else
                {
                    commandResult.Errors = new List<string> { "Critical error: Card not found." };
                }

            }

            return commandResult;
        }
    }
}