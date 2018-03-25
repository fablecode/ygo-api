using System.Threading.Tasks;
using MediatR;

namespace ygo.application.Commands.UpdateArchetype
{
    public class UpdateArchetypeCommandHandler : IAsyncRequestHandler<UpdateArchetypeCommand, CommandResult>
    {
        public Task<CommandResult> Handle(UpdateArchetypeCommand message)
        {
            var commandResult = new CommandResult();


        }
    }
}