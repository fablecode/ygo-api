using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ygo.application.Repository;
using ygo.domain.Models;
using ygo.infrastructure.Database;

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