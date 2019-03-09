using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class CardTipService : ICardTipService
    {
        private readonly ICardTipRepository _cardTipRepository;

        public CardTipService(ICardTipRepository cardTipRepository)
        {
            _cardTipRepository = cardTipRepository;
        }

        public Task<List<TipSection>> TipSectionsByCardId(long cardId)
        {
            return _cardTipRepository.TipSectionsByCardId(cardId);
        }

        public Task DeleteByCardId(long cardId)
        {
            return _cardTipRepository.DeleteByCardId(cardId);
        }

        public Task Update(List<TipSection> tipSections)
        {
            return _cardTipRepository.Update(tipSections);
        }
    }
}