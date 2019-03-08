using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;

namespace ygo.domain.Services
{
    public class ArchetypeSupportCardsService : IArchetypeSupportCardsService
    {
        private readonly IArchetypeSupportCardsService _archetypeSupportCardsService;

        public ArchetypeSupportCardsService(IArchetypeSupportCardsService archetypeSupportCardsService)
        {
            _archetypeSupportCardsService = archetypeSupportCardsService;
        }
        public Task<IEnumerable<ArchetypeCard>> Update(long archetypeId, IEnumerable<string> cards)
        {
            return _archetypeSupportCardsService.Update(archetypeId, cards);
        }
    }
}