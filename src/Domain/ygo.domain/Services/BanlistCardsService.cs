using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class BanlistCardsService : IBanlistCardsService
    {
        private readonly IBanlistCardsRepository _banlistCardsRepository;

        public BanlistCardsService(IBanlistCardsRepository banlistCardsRepository)
        {
            _banlistCardsRepository = banlistCardsRepository;
        }

        public Task<ICollection<BanlistCard>> Update(long banlistId, BanlistCard[] banlistCards)
        {
            return _banlistCardsRepository.Update(banlistId, banlistCards);
        }
    }
}