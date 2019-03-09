using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class CardTriviaService : ICardTriviaService
    {
        private readonly ICardTriviaRepository _cardTriviaRepository;

        public CardTriviaService(ICardTriviaRepository cardTriviaRepository)
        {
            _cardTriviaRepository = cardTriviaRepository;
        }
        public Task<List<TriviaSection>> TriviaSectionsByCardId(long cardId)
        {
            return _cardTriviaRepository.TriviaSectionsByCardId(cardId);
        }

        public Task DeleteByCardId(long cardId)
        {
            return _cardTriviaRepository.DeleteByCardId(cardId);
        }

        public Task Update(List<TriviaSection> triviaSections)
        {
            return _cardTriviaRepository.Update(triviaSections);
        }
    }
}