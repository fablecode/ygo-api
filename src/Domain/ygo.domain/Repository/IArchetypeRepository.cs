using System.Threading.Tasks;
using ygo.infrastructure.Models;

namespace ygo.domain.Repository
{
    public interface IArchetypeRepository
    {
        Task<Archetype> ArchetypeByName(string name);
        Task<Archetype> ArchetypeById(long id);
        Task<Archetype> Add(Archetype archetype);
        Task<Archetype> Update(Archetype archetype);
    }
}