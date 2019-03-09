using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Task<List<Category>> AllCategories()
        {
            return _categoryRepository.AllCategories();
        }

        public Task<Category> CategoryById(int id)
        {
            return _categoryRepository.CategoryById(id);
        }

        public Task<Category> Add(Category category)
        {
            return _categoryRepository.Add(category);
        }
    }
}