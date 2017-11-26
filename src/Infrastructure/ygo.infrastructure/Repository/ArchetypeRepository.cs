using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class ArchetypeRepository : IArchetypeRepository
    {
        private readonly YgoDbContext _dbContext;

        public ArchetypeRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Archetype> ArchetypeByName(string name)
        {
            return _dbContext
                    .Archetype
                        .Include(ac => ac.ArchetypeCard)
                    .SingleOrDefaultAsync(a => a.Name == name);
        }
    }
}