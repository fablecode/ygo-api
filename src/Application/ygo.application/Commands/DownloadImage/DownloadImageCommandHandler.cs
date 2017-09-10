using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.application.Service;
using System.IO;

namespace ygo.application.Commands.DownloadImage
{
    public class DownloadImageCommandHandler : IAsyncRequestHandler<DownloadImageCommand, CommandResult>
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IValidator<DownloadImageCommand> _validator;

        public DownloadImageCommandHandler(IFileSystemService fileSystemService, IValidator<DownloadImageCommand> validator)
        {
            _fileSystemService = fileSystemService;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(DownloadImageCommand message)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(message);

            if (validationResult.IsValid)
            {
                var existingImages = _fileSystemService.GetFiles(Path.GetDirectoryName(message.LocalImageFileName), message.FileName + ".*");
                existingImages.ToList().ForEach(_fileSystemService.Delete);

                commandResult.Data = await _fileSystemService.Download(message.RemoteImageUrl, message.LocalImageFileName);
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