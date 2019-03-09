using System.Threading.Tasks;
using ygo.core.Enums;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.core.Strategies;
using ygo.domain.Mappers;
using ygo.domain.Repository;

namespace ygo.domain.Strategies
{
    public class SpellCardTypeStrategy : ICardTypeStrategy
    {
        private readonly ICardRepository _cardRepository;

        public SpellCardTypeStrategy(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<Card> Add(CardModel cardModel)
        {
            var newSpellCard = CardMapper.MapToSpellOrTrapCard(cardModel);

            return await _cardRepository.Add(newSpellCard);
        }
        public async Task<Card> Update(CardModel cardModel)
        {
            var cardToUpdate = await _cardRepository.CardById(cardModel.Id);

            if (cardToUpdate != null)
            {
                CardMapper.UpdateSpellCardWith(cardToUpdate, cardModel);
                return await _cardRepository.Update(cardToUpdate);
            }

            return null;
        }
        public bool Handles(YugiohCardType yugiohCardType)
        {
            return yugiohCardType == YugiohCardType.Spell;
        }

    }
}