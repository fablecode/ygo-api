using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.infrastructure.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly YgoDbContext _dbContext;

        public CategoryRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Category>> AllCategories()
        {
            return _dbContext.Category.OrderBy(c => c.Name).ToListAsync();
        }

        public Task<Category> CategoryById(int id)
        {
            return _dbContext.Category.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> Add(Category category)
        {
            await _dbContext.Category.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }
    }
}