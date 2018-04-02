using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.AllSubCategories
{
    public class AllSubCategoriesQueryHandler : IRequestHandler<AllSubCategoriesQuery, IEnumerable<SubCategoryDto>>
    {
        private readonly ISubCategoryRepository _repository;

        public AllSubCategoriesQueryHandler(ISubCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SubCategoryDto>> Handle(AllSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            var subCategories = await _repository.AllSubCategories();

            return Mapper.Map<IEnumerable<SubCategoryDto>>(subCategories);
        }
    }
}