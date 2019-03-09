using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class ArchetypeService : IArchetypeService
    {
        private readonly IArchetypeRepository _archetypeRepository;

        public ArchetypeService(IArchetypeRepository archetypeRepository)
        {
            _archetypeRepository = archetypeRepository;
        }
        public Task<Archetype> ArchetypeByName(string name)
        {
            return _archetypeRepository.ArchetypeByName(name);
        }

        public Task<Archetype> ArchetypeById(long id)
        {
            return _archetypeRepository.ArchetypeById(id);
        }

        public Task<Archetype> Add(Archetype archetype)
        {
            return _archetypeRepository.Add(archetype);
        }

        public Task<Archetype> Update(Archetype archetype)
        {
            return _archetypeRepository.Update(archetype);
        }

        public Task<IEnumerable<string>> Names(string filter)
        {
            return _archetypeRepository.Names(filter);
        }

        public Task<SearchResult<Archetype>> Search(string searchTerm, int pageNumber, int pageSize)
        {
            return _archetypeRepository.Search(searchTerm, pageNumber, pageSize);
        }
    }
}