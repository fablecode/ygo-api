using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo.domain.Repository;
using ygo.infrastructure.Database;
using ygo.infrastructure.Models;

namespace ygo.infrastructure.Repository
{
    public class LimitRepository : ILimitRepository
    {
        private readonly YgoDbContext _dbContext;

        public LimitRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Limit>> AllLimits()
        {
            return _dbContext.Limit.OrderBy(c => c.Name).ToListAsync();
        }
    }
}