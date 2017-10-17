using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> AllCategories();
        Task<Category> CategoryById(int id);
        Task<Category> Add(Category category);
    }
}