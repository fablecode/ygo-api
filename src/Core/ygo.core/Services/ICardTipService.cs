using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.core.Services
{
    public interface ICardTipService
    {
        Task<List<TipSection>> TipSectionsByCardId(long cardId);
        Task DeleteByCardId(long cardId);
        Task Update(List<TipSection> tipSections);
    }
}