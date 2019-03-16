using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.LatestBanlistByFormat
{
    public class LatestBanlistQueryHandler : IRequestHandler<LatestBanlistQuery, LatestBanlistDto>
    {
        private readonly IBanlistService _banlistService;

        public LatestBanlistQueryHandler(IBanlistService banlistService)
        {
            _banlistService = banlistService;
        }

        public async Task<LatestBanlistDto> Handle(LatestBanlistQuery request, CancellationToken cancellationToken)
        {
            var banlist = await _banlistService.GetBanlistByFormatAcronym(request.Acronym.ToString());

            return QueryMapperHelper.MapToLatestBanlist(banlist);
        }
    }
}