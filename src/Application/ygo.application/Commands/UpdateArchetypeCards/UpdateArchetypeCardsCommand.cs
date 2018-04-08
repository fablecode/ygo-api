using System.Collections.Generic;
using MediatR;

namespace ygo.application.Commands.UpdateArchetypeCards
{
    public class UpdateArchetypeCardsCommand : IRequest<CommandResult>
    {
        public long ArchetypeId { get; set; }
        public IEnumerable<string> Cards { get; set; }
    }
}