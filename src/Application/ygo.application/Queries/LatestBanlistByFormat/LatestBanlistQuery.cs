using MediatR;
using ygo.application.Dto;
using ygo.application.Enums;

namespace ygo.application.Queries.LatestBanlistByFormat
{
    public class LatestBanlistQuery : IRequest<LatestBanlistDto>
    {
        public BanlistFormat Acronym { get; set; }
    }
}