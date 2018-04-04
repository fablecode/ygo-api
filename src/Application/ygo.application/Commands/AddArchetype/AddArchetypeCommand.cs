using System;
using System.Collections.Generic;
using MediatR;

namespace ygo.application.Commands.AddArchetype
{
    public class AddArchetypeCommand : IRequest<CommandResult>
    {
        public long ArchetypeNumber { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string ProfileUrl { get; set; }
        public IEnumerable<string> Cards { get; set; }
    }
}