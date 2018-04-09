using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class FormatRepository : IFormatRepository
    {
        private readonly YgoDbContext _dbContext;

        public FormatRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Format> FormatByAcronym(string acronym)
        {
            return _dbContext
                        .Format
                        .AsNoTracking()
                        .SingleOrDefaultAsync(f => f.Acronym == acronym);
        }
    }
}