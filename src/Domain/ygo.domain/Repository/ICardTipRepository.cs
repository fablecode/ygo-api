using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface ICardTipRepository
    {
        Task<List<TipSection>> TipSectionsByCardId(long cardId);
        Task DeleteByCardId(long cardId);
        Task Update(List<TipSection> tipSections);
    }
}