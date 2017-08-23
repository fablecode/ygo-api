using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategories()
        {
            throw new System.NotImplementedException();
        }
    }
}