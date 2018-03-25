using System.Collections.Generic;
using MediatR;

namespace ygo.application.Commands.AddArchetype
{
    public class AddArchetypeCommand : IRequest<CommandResult>
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public int ArchetypeNumber { get; set; }
        public IEnumerable<string> Cards { get; set; }
    }
}