using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface IArchetypeSupportCardsRepository
    {
        Task<IEnumerable<ArchetypeCard>> Update(long archetypeId, IEnumerable<string> cards);
    }
}