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
                    .SingleOrDefaultAsync(a => a.Name == name);
        }

        public Task<Archetype> ArchetypeById(long id)
        {
            return _dbContext
                    .Archetype
                    .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Archetype> Add(Archetype archetype)
        {
            await _dbContext.Archetype.AddAsync(archetype);
            await _dbContext.SaveChangesAsync();

            return archetype;
        }

        public async Task<Archetype> Update(Archetype archetype)
        {
            _dbContext.Archetype.Update(archetype);

            await _dbContext.SaveChangesAsync();

            return archetype;
        }
    }
}