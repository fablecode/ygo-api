using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.core.Services
{
    public interface ICardRulingService
    {
        Task<List<RulingSection>> RulingSectionsByCardId(long cardId);
        Task DeleteByCardId(long cardId);
        Task Update(List<RulingSection> rulingSections);
    }
}