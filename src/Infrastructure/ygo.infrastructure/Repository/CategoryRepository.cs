using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo.domain.Repository;
using ygo.infrastructure.Database;
using ygo.infrastructure.Models;

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