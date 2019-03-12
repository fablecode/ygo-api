using MediatR;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Services;

namespace ygo.application.Commands.DeleteFile
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, CommandResult>
    {
        private readonly IFileSystemService _fileSystemService;

        public DeleteFileCommandHandler(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        public Task<CommandResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            if (!string.IsNullOrWhiteSpace(request.LocalFileNameFullPath))
            {
                _fileSystemService.Delete(request.LocalFileNameFullPath);

                commandResult.IsSuccessful = true;
            }
            else
            {
                commandResult.Errors =  new List<string>{ "LocalFileNameFullPath cannot be null of empty" };
            }

            return Task.FromResult(commandResult);
        }
    }
}