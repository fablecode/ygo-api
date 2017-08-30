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
        private readonly YgoDbContext _dbContext;

        public CategoryRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Category>> AllCategories()
        {
            var orderedQueryable = from c in _dbContext.Category orderby c.Name select c;

            return orderedQueryable.ToListAsync();
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