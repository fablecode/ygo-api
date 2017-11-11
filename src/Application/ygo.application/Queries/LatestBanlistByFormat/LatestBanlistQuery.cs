using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.LatestBanlistByFormat
{
    public class LatestBanlistQuery : IRequest<LatestBanlistDto>
    {
        public string Acronym { get; set; }
    }
}