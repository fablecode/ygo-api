using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class CardService : ICardService
    {
        private readonly IEnumerable<ICardTypeStrategy> _cardTypeStrategies;
        private readonly ICardRepository _cardRepository;

        public CardService(IEnumerable<ICardTypeStrategy> cardTypeStrategies, ICardRepository cardRepository)
        {
            _cardTypeStrategies = cardTypeStrategies;
            _cardRepository = cardRepository;
        }

        public async Task<Card> Add(CardModel cardModel)
        {
            var handler = _cardTypeStrategies.Single(cts => cts.Handles(cardModel.CardType));

            return await handler.Add(cardModel);
        }
        public async Task<Card> Update(CardModel cardModel)
        {
            var handler = _cardTypeStrategies.Single(cts => cts.Handles(cardModel.CardType));

            return await handler.Update(cardModel);
        }

        public Task<Card> CardById(long cardId)
        {
            return _cardRepository.CardById(cardId);
        }
    }
}