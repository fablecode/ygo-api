using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;

namespace ygo.domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryService _categoryService;

        public CategoryService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public Task<List<Category>> AllCategories()
        {
            return _categoryService.AllCategories();
        }

        public Task<Category> CategoryById(int id)
        {
            return _categoryService.CategoryById(id);
        }

        public Task<Category> Add(Category category)
        {
            return _categoryService.Add(category);
        }
    }
}