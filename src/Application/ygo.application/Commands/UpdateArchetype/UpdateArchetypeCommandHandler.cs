using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ygo.application.Commands.UpdateArchetype
{
    public class UpdateArchetypeCommandHandler : IRequestHandler<UpdateArchetypeCommand, CommandResult>
    {
        public Task<CommandResult> Handle(UpdateArchetypeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}