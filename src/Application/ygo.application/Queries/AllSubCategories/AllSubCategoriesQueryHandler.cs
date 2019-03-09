using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.application.Queries.AllSubCategories
{
    public class AllSubCategoriesQueryHandler : IRequestHandler<AllSubCategoriesQuery, IEnumerable<SubCategoryDto>>
    {
        private readonly ISubCategoryService _subCategoryService;

        public AllSubCategoriesQueryHandler(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        public async Task<IEnumerable<SubCategoryDto>> Handle(AllSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            var subCategories = await _subCategoryService.AllSubCategories();

            return Mapper.Map<IEnumerable<SubCategoryDto>>(subCategories);
        }
    }
}