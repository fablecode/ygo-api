using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Services;

namespace ygo.application.Queries.BanlistExists
{
    public class BanlistExistsQueryHandler : IRequestHandler<BanlistExistsQuery, bool>
    {
        private readonly IBanlistService _banlistService;

        public BanlistExistsQueryHandler(IBanlistService banlistService)
        {
            _banlistService = banlistService;
        }

        public Task<bool> Handle(BanlistExistsQuery request, CancellationToken cancellationToken)
        {
            return _banlistService.BanlistExist(request.Id);
        }
    }
}