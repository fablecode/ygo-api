using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.core.Services;

namespace ygo.domain.Services
{
    public class CardService : ICardService
    {
        private readonly IEnumerable<ICardTypeStrategy> _cardTypeStrategies;

        public CardService(IEnumerable<ICardTypeStrategy> cardTypeStrategies)
        {
            _cardTypeStrategies = cardTypeStrategies;
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
    }
}