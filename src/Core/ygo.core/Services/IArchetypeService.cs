using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models;
using ygo.core.Models.Db;

namespace ygo.core.Services
{
    public interface IArchetypeService
    {
        Task<Archetype> ArchetypeByName(string name);
        Task<Archetype> ArchetypeById(long id);
        Task<Archetype> Add(Archetype archetype);
        Task<Archetype> Update(Archetype archetype);
        Task<IEnumerable<string>> Names(string filter);
        Task<SearchResult<Archetype>> Search(string searchTerm, int pageNumber, int pageSize);
    }
}