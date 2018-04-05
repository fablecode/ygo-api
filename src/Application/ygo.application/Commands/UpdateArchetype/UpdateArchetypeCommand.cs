using System.Collections.Generic;
using MediatR;

namespace ygo.application.Commands.UpdateArchetype
{
    public class UpdateArchetypeCommand : IRequest<CommandResult>
    {
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string ProfileUrl { get; set; }
        public IEnumerable<string> Cards { get; set; }
    }
}