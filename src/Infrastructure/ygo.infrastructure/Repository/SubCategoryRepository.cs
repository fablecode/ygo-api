using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ygo.application.Repository;
using ygo.domain.Models;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly YgoDbContext _dbContext;

        public SubCategoryRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SubCategory>> AllSubCategories()
        {
            return await _dbContext.SubCategory.OrderBy(c => c.Name).ToListAsync();

        }
    }
}