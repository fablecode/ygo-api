using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.AllCategories
{
    public class AllCategoriesQueryHandler : IRequestHandler<AllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;

        public AllCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(AllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var allCategories = await _repository.AllCategories();

            return Mapper.Map<IEnumerable<CategoryDto>>(allCategories);
        }
    }
}