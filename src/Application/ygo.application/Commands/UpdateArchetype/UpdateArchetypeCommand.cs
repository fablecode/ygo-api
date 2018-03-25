using System.Collections.Generic;
using MediatR;

namespace ygo.application.Commands.UpdateArchetype
{
    public class UpdateArchetypeCommand : IRequest<CommandResult>
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public int ArchetypeNumber { get; set; }
        public IEnumerable<string> Cards { get; set; }
    }
}