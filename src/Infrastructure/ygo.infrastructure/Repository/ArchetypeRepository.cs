using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ygo.core.Models;
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

        public async Task<IEnumerable<string>> Names(string filter)
        {
            return await (
                            from a in _dbContext.Archetype
                            where EF.Functions.Like(a.Name, $"%{filter}%")
                            select a.Name
                         )
                         .ToListAsync();
        }

        public async Task<SearchResult<Archetype>> Search(string searchTerm, int pageNumber, int pageSize)
        {
            var searchResults = new SearchResult<Archetype>();

            var query = _dbContext
                        .Archetype
                        .Select(a => a)
                            .Include(a => a.ArchetypeCard)
                        .Where(a => EF.Functions.Like(a.Name, $"%{a.Name}%"))
                        .Skip(pageSize * (pageNumber - 1))
                        .Take(pageSize);

            searchResults.Items = await query.ToListAsync();
            searchResults.TotalRecords = await _dbContext.Archetype.CountAsync();

            return searchResults;
        }
    }
}