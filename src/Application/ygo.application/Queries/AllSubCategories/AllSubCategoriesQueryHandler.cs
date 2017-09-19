using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ygo.application.Dto;
using ygo.application.Ioc;
using ygo.application.Repository;

namespace ygo.application.Queries.AllSubCategories
{
    public class AllSubCategoriesQueryHandler : IAsyncRequestHandler<AllSubCategoriesQuery, IEnumerable<SubCategoryDto>>
    {
        private readonly ISubCategoryRepository _repository;

        public AllSubCategoriesQueryHandler(ISubCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SubCategoryDto>> Handle(AllSubCategoriesQuery message)
        {
            var subCategories = await _repository.AllSubCategories();

            return Mapper.Map<IEnumerable<SubCategoryDto>>(subCategories);
        }
    }
}