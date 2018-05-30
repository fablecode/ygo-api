using System.Collections.Generic;
using MediatR;
using ygo.application.Dto;

namespace ygo.application.Commands.UpdateTips
{
    public class UpdateTipsCommand : IRequest<CommandResult>
    {
        public long CardId { get; set; }
        public List<TipSectionDto> Tips { get; set; }
    }
}