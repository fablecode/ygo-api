using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface ICardRulingRepository
    {
        Task<List<RulingSection>> RulingSectionsByCardId(long cardId);
        Task DeleteByCardId(long cardId);
        Task Update(List<RulingSection> rulingSections);
    }
}