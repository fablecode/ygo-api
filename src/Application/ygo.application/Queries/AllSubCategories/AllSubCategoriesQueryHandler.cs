using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.Queries.AllSubCategories
{
    public class AllSubCategoriesQueryHandler : IAsyncRequestHandler<AllSubCategoriesQuery, IEnumerable<SubCategory>>
    {
        private readonly ISubCategoryRepository _repository;

        public AllSubCategoriesQueryHandler(ISubCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SubCategory>> Handle(AllSubCategoriesQuery message)
        {
            return await _repository.AllSubCategories();
        }
    }
}