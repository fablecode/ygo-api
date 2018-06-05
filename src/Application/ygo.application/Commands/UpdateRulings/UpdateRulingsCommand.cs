using System.Collections.Generic;
using MediatR;
using ygo.application.Dto;

namespace ygo.application.Commands.UpdateRulings
{
    public class UpdateRulingCommand : IRequest<CommandResult>
    {
        public long CardId { get; set; }
        public List<RulingSectionDto> Rulings { get; set; }
    }
}