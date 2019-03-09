using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.core.Services
{
    public interface IArchetypeSupportCardsService
    {
        Task<IEnumerable<ArchetypeCard>> Update(long archetypeId, IEnumerable<string> cards);
    }
}