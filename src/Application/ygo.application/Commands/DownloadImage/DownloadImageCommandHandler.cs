using FluentValidation;
using MediatR;
using MimeTypes;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.domain.SystemIO;

namespace ygo.application.Commands.DownloadImage
{
    public class DownloadImageCommandHandler : IRequestHandler<DownloadImageCommand, CommandResult>
    {
        private readonly IFileSystem _fileSystem;
        private readonly IValidator<DownloadImageCommand> _validator;

        public DownloadImageCommandHandler(IFileSystem fileSystem, IValidator<DownloadImageCommand> validator)
        {
            _fileSystem = fileSystem;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(DownloadImageCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid)
            {
                var imageFileFullPathWithoutExtension = Path.Combine(request.ImageFolderPath, Path.GetFileNameWithoutExtension(request.ImageFileName));

                var downloadedFileResult = await _fileSystem.Download(request.RemoteImageUrl, imageFileFullPathWithoutExtension);

                var imageFileFullPathWithExtension = string.Concat(imageFileFullPathWithoutExtension, GetDefaultExtension(downloadedFileResult.ContentType));

                if (_fileSystem.Exists(imageFileFullPathWithExtension))
                    _fileSystem.Delete(imageFileFullPathWithExtension);

                _fileSystem.Rename(imageFileFullPathWithoutExtension, imageFileFullPathWithExtension);

                commandResult.Data = downloadedFileResult;
                commandResult.IsSuccessful = true;
            }
            else
            {
                commandResult.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }

        private string GetDefaultExtension(string mimeType)
        {
            return MimeTypeMap.GetExtension(mimeType);
        }
    }
}