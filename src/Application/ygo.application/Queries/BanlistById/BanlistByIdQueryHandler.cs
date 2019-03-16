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
        private readonly IMapper _mapper;

        public BanlistByIdQueryHandler(IBanlistService banlistService, IMapper mapper)
        {
            _banlistService = banlistService;
            _mapper = mapper;
        }

        public async Task<BanlistDto> Handle(BanlistByIdQuery request, CancellationToken cancellationToken)
        {
            var banlist = await _banlistService.GetBanlistById(request.Id);

            if (banlist != null)
            {
                return _mapper.Map<BanlistDto>(banlist);
            }

            return null;
        }
    }
}