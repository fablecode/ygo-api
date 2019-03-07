using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.core.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> AllCategories();
        Task<Category> CategoryById(int id);
        Task<Category> Add(Category category);
    }
}