using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.AllCategories
{
    public class AllCategoriesQueryHandler : IRequestHandler<AllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public AllCategoriesQueryHandler(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(AllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var allCategories = await _categoryService.AllCategories();

            return _mapper.Map<IEnumerable<CategoryDto>>(allCategories);
        }
    }
}