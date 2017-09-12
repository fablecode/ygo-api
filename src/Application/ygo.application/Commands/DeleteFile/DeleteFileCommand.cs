using MediatR;

namespace ygo.application.Commands.DeleteFile
{
    public class DeleteFileCommand : IRequest<CommandResult>
    {
        public string LocalFileNameFullPath { get; set; }
    }
}