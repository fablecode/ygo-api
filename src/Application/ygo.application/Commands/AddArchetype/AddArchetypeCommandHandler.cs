using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.DownloadImage;
using ygo.domain.Repository;
using ygo.infrastructure.Models;

namespace ygo.application.Commands.AddArchetype
{
    public class AddArchetypeCommandHandler : IRequestHandler<AddArchetypeCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddArchetypeCommand> _validator;
        private readonly IArchetypeRepository _archetypeRepository;
        private readonly IOptions<ApplicationSettings> _settings;

        public AddArchetypeCommandHandler
        (
            IMediator mediator, 
            IValidator<AddArchetypeCommand> validator, 
            IArchetypeRepository archetypeRepository, 
            IOptions<ApplicationSettings> settings
        )
        {
            _mediator = mediator;
            _validator = validator;
            _archetypeRepository = archetypeRepository;
            _settings = settings;
        }

        public async Task<CommandResult> Handle(AddArchetypeCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResult();

            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid)
            {
                var newArchetype = await _archetypeRepository.Add(new Archetype
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

                response.Data = newArchetype.Id;
                response.IsSuccessful = true;
            }

            return response;
        }
    }
}