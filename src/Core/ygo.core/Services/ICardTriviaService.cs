using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.core.Services
{
    public interface ICardTriviaService
    {
        Task<List<TriviaSection>> TriviaSectionsByCardId(long cardId);
        Task DeleteByCardId(long cardId);
        Task Update(List<TriviaSection> triviaSections);
    }
}