using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface IArchetypeRepository
    {
        Task<Archetype> ArchetypeByName(string name);
        Task<Archetype> ArchetypeById(long id);
    }
}