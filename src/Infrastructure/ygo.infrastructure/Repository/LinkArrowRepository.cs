using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo.domain.Repository;
using ygo.infrastructure.Database;
using ygo.infrastructure.Models;

namespace ygo.infrastructure.Repository
{
    public class LinkArrowRepository : ILinkArrowRepository
    {
        private readonly YgoDbContext _dbContext;

        public LinkArrowRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<LinkArrow>> AllLinkArrows()
        {
            return _dbContext.LinkArrow.OrderBy(la => la.Name).ToListAsync();
        }
    }
}