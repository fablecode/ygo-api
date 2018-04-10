using System.Collections.Generic;
using MediatR;

namespace ygo.application.Commands.UpdateArchetypeSupportCards
{
    public class UpdateArchetypeSupportCardsCommand : IRequest<CommandResult>
    {
        public long ArchetypeId { get; set; }
        public IEnumerable<string> Cards { get; set; }
    }
}