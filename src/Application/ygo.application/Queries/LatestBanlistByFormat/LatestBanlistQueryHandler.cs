using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.LatestBanlistByFormat
{
    public class LatestBanlistQueryHandler : IRequestHandler<LatestBanlistQuery, LatestBanlistDto>
    {
        private readonly IBanlistRepository _banlistRepository;

        public LatestBanlistQueryHandler(IBanlistRepository banlistRepository)
        {
            _banlistRepository = banlistRepository;
        }

        public async Task<LatestBanlistDto> Handle(LatestBanlistQuery request, CancellationToken cancellationToken)
        {
            var banlist = await _banlistRepository.GetBanlistByFormatAcronym(request.Acronym);

            return banlist?.MapToLatestBanlist();
        }
    }
}