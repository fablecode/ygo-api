using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface ICardTriviaRepository
    {
        Task<List<TriviaSection>> TriviaSectionsByCardId(long cardId);
        Task DeleteByCardId(long cardId);
        Task Update(List<TriviaSection> triviaSections);
    }
}