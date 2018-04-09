using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo.domain.Repository;
using ygo.infrastructure.Database;
using Attribute = ygo.core.Models.Db.Attribute;

namespace ygo.infrastructure.Repository
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly YgoDbContext _dbContext;

        public AttributeRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Attribute>> AllAttributes()
        {
            return _dbContext.Attribute.OrderBy(c => c.Name).ToListAsync();
        }

    }
}