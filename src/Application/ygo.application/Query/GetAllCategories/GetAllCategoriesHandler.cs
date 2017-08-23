using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.Query.GetAllCategories
{
    public class GetAllCategoriesHandler : IAsyncRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _repository;

        public GetAllCategoriesHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery message)
        {
            return _repository.GetAllCategories();
        }
    }
}