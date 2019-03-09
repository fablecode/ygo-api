using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class ArchetypeSupportCardsService : IArchetypeSupportCardsService
    {
        private readonly IArchetypeSupportCardsRepository _archetypeSupportCardsRepository;

        public ArchetypeSupportCardsService(IArchetypeSupportCardsRepository archetypeSupportCardsRepository)
        {
            _archetypeSupportCardsRepository = archetypeSupportCardsRepository;
        }
        public Task<IEnumerable<ArchetypeCard>> Update(long archetypeId, IEnumerable<string> cards)
        {
            return _archetypeSupportCardsRepository.Update(archetypeId, cards);
        }
    }
}