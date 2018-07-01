using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo.domain.Repository;
using ygo.infrastructure.Database;
using Type = ygo.core.Models.Db.Type;

namespace ygo.infrastructure.Repository
{
    public class TypeRepository : ITypeRepository
    {
        private readonly YgoDbContext _dbContext;

        public TypeRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Type>> AllTypes()
        {
            return _dbContext.Type.OrderBy(t => t.Name).ToListAsync();
        }
    }
}