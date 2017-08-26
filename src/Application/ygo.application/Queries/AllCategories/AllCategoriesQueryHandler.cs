using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.Queries.AllCategories
{
    public class AllCategoriesQueryHandler : IAsyncRequestHandler<AllCategoriesQuery, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _repository;

        public AllCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Category>> Handle(AllCategoriesQuery message)
        {
            return await _repository.GetAllCategories();
        }
    }
}