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

        public Task<List<SubCategory>> AllSubCategories()
        {
            return _dbContext
                        .SubCategory
                        .Include(sc => sc.Category)
                        .Select(sc => new SubCategory
                        {
                            Id = sc.Id,
                            CategoryId = sc.Category.Id,
                            Category = sc.Category,
                            Name = sc.Name,
                            Created = sc.Created,
                            Updated = sc.Updated
                        })
                        .OrderBy(sc => sc.Name)
                        .ToListAsync();

        }
    }
}