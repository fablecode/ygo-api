using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.BanlistById
{
    public class BanlistByIdQueryHandler : IRequestHandler<BanlistByIdQuery, BanlistDto>
    {
        private readonly IBanlistService _banlistService;

        public BanlistByIdQueryHandler(IBanlistService banlistService)
        {
            _banlistService = banlistService;
        }

        public async Task<BanlistDto> Handle(BanlistByIdQuery request, CancellationToken cancellationToken)
        {
            var banlist = await _banlistService.GetBanlistById(request.Id);

            if (banlist != null)
            {
                return Mapper.Map<BanlistDto>(banlist);
            }

            return null;
        }
    }
}