using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.application.Repository;
using ygo.domain.Models;
using ygo.infrastructure.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ygo.infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IYgoDbContext _dbContext;

        public CategoryRepository(IYgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var orderedQueryable = from c in _dbContext.Categories orderby c.Name select c;

            return await orderedQueryable.ToListAsync();
        }
    }
}