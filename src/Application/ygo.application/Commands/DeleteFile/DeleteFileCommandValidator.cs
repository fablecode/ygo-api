using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.domain.Service;

namespace ygo.application.Commands.DeleteFile
{
    public class DeleteFileCommandValidator : IRequestHandler<DeleteFileCommand, CommandResult>
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IValidator<DeleteFileCommand> _validator;

        public DeleteFileCommandValidator(IFileSystemService fileSystemService, IValidator<DeleteFileCommand> validator)
        {
            _fileSystemService = fileSystemService;
            _validator = validator;
        }

        public Task<CommandResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                _fileSystemService.Delete(request.LocalFileNameFullPath);
            }
            else
            {
                commandResult.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return Task.FromResult(commandResult);
        }
    }
}