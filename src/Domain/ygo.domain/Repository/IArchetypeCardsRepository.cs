using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.infrastructure.Models;

namespace ygo.domain.Repository
{
    public interface IArchetypeCardsRepository
    {
        Task<IEnumerable<ArchetypeCard>> Update(long archetypeId, IEnumerable<string> cards);
    }
}