using System.Collections.Generic;
using MediatR;
using ygo.application.Dto;

namespace ygo.application.Commands.UpdateTrivias
{
    public class UpdateTriviaCommand : IRequest<CommandResult>
    {
        public long CardId { get; set; }
        public List<TriviaSectionDto> Tips { get; set; }
    }
}