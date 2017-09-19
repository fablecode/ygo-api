using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ygo.application.Dto;
using ygo.application.Ioc;
using ygo.application.Repository;

namespace ygo.application.Queries.AllCategories
{
    public class AllCategoriesQueryHandler : IAsyncRequestHandler<AllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;

        public AllCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(AllCategoriesQuery message)
        {
            var allCategories = await _repository.AllCategories();

            return Mapper.Map<IEnumerable<CategoryDto>>(allCategories);
        }
    }
}