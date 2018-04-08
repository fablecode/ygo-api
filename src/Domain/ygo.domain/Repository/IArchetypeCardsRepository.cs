using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface IArchetypeCardsRepository
    {
        Task<IEnumerable<ArchetypeCard>> Update(long archetype, IEnumerable<string> cards);
    }
}