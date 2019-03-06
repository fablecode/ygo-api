using System;
using System.Threading.Tasks;
using ygo.core.Enums;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Mappers;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class MonsterCardTypeStrategy : ICardTypeStrategy
    {
        private readonly ICardRepository _cardRepository;

        public MonsterCardTypeStrategy(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<Card> Add(CardModel cardModel)
        {
            var newMonsterCard = CardMapper.MapToMonsterCard(cardModel);

            return await _cardRepository.Add(newMonsterCard);
        }

        public Task<Card> Update(CardModel cardModel)
        {
            throw new NotImplementedException();
        }

        public bool Handles(YugiohCardType yugiohCardType)
        {
            return yugiohCardType == YugiohCardType.Monster;
        }
    }
}