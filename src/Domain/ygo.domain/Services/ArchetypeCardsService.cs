using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public sealed class ArchetypeCardsService : IArchetypeCardsService
    {
        private readonly IArchetypeCardsRepository _archetypeCardsRepository;

        public ArchetypeCardsService(IArchetypeCardsRepository archetypeCardsRepository)
        {
            _archetypeCardsRepository = archetypeCardsRepository;
        }
        public Task<IEnumerable<ArchetypeCard>> Update(long archetypeId, IEnumerable<string> cards)
        {
            return _archetypeCardsRepository.Update(archetypeId, cards);
        }
    }
}