using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.AllSubCategories
{
    public class AllSubCategoriesQueryHandler : IRequestHandler<AllSubCategoriesQuery, IEnumerable<SubCategoryDto>>
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly IMapper _mapper;

        public AllSubCategoriesQueryHandler(ISubCategoryService subCategoryService, IMapper mapper)
        {
            _subCategoryService = subCategoryService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubCategoryDto>> Handle(AllSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            var subCategories = await _subCategoryService.AllSubCategories();

            return _mapper.Map<IEnumerable<SubCategoryDto>>(subCategories);
        }
    }
}