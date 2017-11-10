using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.LatestBanlistByFormat
{
    public class LatestBanlistByFormatQuery : IRequest<LatestBanlistDto>
    {
        public string Format { get; set; }
    }
}