using System.Collections.Generic;
using MediatR;
using ygo.application.Dto;

namespace ygo.application.Commands.UpdateBanlistCards
{
    public class UpdateBanlistCardsCommand : IRequest<CommandResult>
    {
        public long BanlistId { get; set; }
        public IEnumerable<BanlistCardDto> BanlistCards { get; set; }
    }
}