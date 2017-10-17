using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using MimeTypes;
using ygo.domain.Service;

namespace ygo.application.Commands.DownloadImage
{
    public class DownloadImageCommandHandler : IAsyncRequestHandler<DownloadImageCommand, CommandResult>
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IValidator<DownloadImageCommand> _validator;
        private readonly IOptions<ApplicationSettings> _settings;

        public DownloadImageCommandHandler(IFileSystemService fileSystemService, IValidator<DownloadImageCommand> validator, IOptions<ApplicationSettings> settings)
        {
            _fileSystemService = fileSystemService;
            _validator = validator;
            _settings = settings;
        }

        public async Task<CommandResult> Handle(DownloadImageCommand message)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(message);

            if (validationResult.IsValid)
            {
                var imageFileWithoutExtensionFullPath = Path.Combine(_settings.Value.CardImageFolderPath, Path.GetFileNameWithoutExtension(message.ImageFileName));

                var downloadedFileResult = await _fileSystemService.Download(message.RemoteImageUrl, imageFileWithoutExtensionFullPath);

                var imageFileWithExtensionFullPath = string.Concat(imageFileWithoutExtensionFullPath, GetDefaultExtension(downloadedFileResult.ContentType));

                if (_fileSystemService.Exists(imageFileWithExtensionFullPath))
                    _fileSystemService.Delete(imageFileWithExtensionFullPath);

                _fileSystemService.Rename(imageFileWithoutExtensionFullPath, imageFileWithExtensionFullPath);

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