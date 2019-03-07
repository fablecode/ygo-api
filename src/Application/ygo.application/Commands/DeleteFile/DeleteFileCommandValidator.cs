using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.domain.SystemIO;

namespace ygo.application.Commands.DeleteFile
{
    public class DeleteFileCommandValidator : IRequestHandler<DeleteFileCommand, CommandResult>
    {
        private readonly IFileSystem _fileSystem;
        private readonly IValidator<DeleteFileCommand> _validator;

        public DeleteFileCommandValidator(IFileSystem fileSystem, IValidator<DeleteFileCommand> validator)
        {
            _fileSystem = fileSystem;
            _validator = validator;
        }

        public Task<CommandResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                _fileSystem.Delete(request.LocalFileNameFullPath);
            }
            else
            {
                commandResult.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return Task.FromResult(commandResult);
        }
    }
}