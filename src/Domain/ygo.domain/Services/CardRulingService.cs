using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class CardRulingService : ICardRulingService
    {
        private readonly ICardRulingRepository _cardRulingRepository;

        public CardRulingService(ICardRulingRepository cardRulingRepository)
        {
            _cardRulingRepository = cardRulingRepository;
        }

        public Task<List<RulingSection>> RulingSectionsByCardId(long cardId)
        {
            return _cardRulingRepository.RulingSectionsByCardId(cardId);
        }

        public Task DeleteByCardId(long cardId)
        {
            return _cardRulingRepository.DeleteByCardId(cardId);
        }

        public Task Update(List<RulingSection> rulingSections)
        {
            return _cardRulingRepository.Update(rulingSections);
        }
    }
}