using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.application.Service;

namespace ygo.application.Commands.DeleteFile
{
    public class DeleteFileCommandValidator : IAsyncRequestHandler<DeleteFileCommand, CommandResult>
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IValidator<DeleteFileCommand> _validator;

        public DeleteFileCommandValidator(IFileSystemService fileSystemService, IValidator<DeleteFileCommand> validator)
        {
            _fileSystemService = fileSystemService;
            _validator = validator;
        }

        public Task<CommandResult> Handle(DeleteFileCommand message)
        { 
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
            {
                _fileSystemService.Delete(message.LocalFileNameFullPath);
            }
            else
            {
                commandResult.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return Task.FromResult(commandResult);
        }
    }
}