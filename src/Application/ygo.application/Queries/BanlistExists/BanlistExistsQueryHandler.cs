using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ygo.domain.Repository;

namespace ygo.application.Queries.BanlistExists
{
    public class BanlistExistsQueryHandler : IRequestHandler<BanlistExistsQuery, bool>
    {
        private readonly IBanlistRepository _banlistRepository;

        public BanlistExistsQueryHandler(IBanlistRepository banlistRepository)
        {
            _banlistRepository = banlistRepository;
        }

        public Task<bool> Handle(BanlistExistsQuery request, CancellationToken cancellationToken)
        {
            return _banlistRepository.BanlistExist(request.Id);
        }
    }
}