using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.BanlistById
{
    public class BanlistByIdQueryHandler : IRequestHandler<BanlistByIdQuery, BanlistDto>
    {
        private readonly IBanlistRepository _banlistRepository;

        public BanlistByIdQueryHandler(IBanlistRepository banlistRepository)
        {
            _banlistRepository = banlistRepository;
        }

        public async Task<BanlistDto> Handle(BanlistByIdQuery request, CancellationToken cancellationToken)
        {
            var banlist = await _banlistRepository.GetBanlistById(request.Id);

            if (banlist != null)
            {
                return Mapper.Map<BanlistDto>(banlist);
            }

            return null;
        }
    }
}