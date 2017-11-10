using System.Threading.Tasks;
using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.LatestBanlistByFormat
{
    public class LatestBanlistByFormatQueryHandler : IAsyncRequestHandler<LatestBanlistByFormatQuery, LatestBanlistDto>
    {
        public Task<LatestBanlistDto> Handle(LatestBanlistByFormatQuery message)
        {
            throw new System.NotImplementedException();
        }
    }
}