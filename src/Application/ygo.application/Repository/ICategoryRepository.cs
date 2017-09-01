using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo.domain.Models;

namespace ygo.application.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> AllCategories();
        Task<Category> CategoryById(int id);
        Task<Category> Add(Category category);
    }
}