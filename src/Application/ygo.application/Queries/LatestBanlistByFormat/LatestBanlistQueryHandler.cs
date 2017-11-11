using System.Threading.Tasks;
using MediatR;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.LatestBanlistByFormat
{
    public class LatestBanlistQueryHandler : IAsyncRequestHandler<LatestBanlistQuery, LatestBanlistDto>
    {
        private readonly IBanlistRepository _banlistRepository;

        public LatestBanlistQueryHandler(IBanlistRepository banlistRepository)
        {
            _banlistRepository = banlistRepository;
        }

        public async Task<LatestBanlistDto> Handle(LatestBanlistQuery message)
        {
            var banlist = await _banlistRepository.GetBanlistByFormatAcronym(message.Acronym);

            return banlist?.MapToLatestBanlist();
        }
    }
}