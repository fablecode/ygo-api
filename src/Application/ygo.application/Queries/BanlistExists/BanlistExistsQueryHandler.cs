using System.Threading.Tasks;
using MediatR;
using ygo.domain.Repository;

namespace ygo.application.Queries.BanlistExists
{
    public class BanlistExistsQueryHandler : IAsyncRequestHandler<BanlistExistsQuery, bool>
    {
        private readonly IBanlistRepository _banlistRepository;

        public BanlistExistsQueryHandler(IBanlistRepository banlistRepository)
        {
            _banlistRepository = banlistRepository;
        }

        public Task<bool> Handle(BanlistExistsQuery message)
        {
            return _banlistRepository.BanlistExist(message.Id);
        }
    }
}