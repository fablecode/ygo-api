using AutoMapper;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.CategoryById
{
    public class CategoryByIdQueryHandler: IRequestHandler<CategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoryByIdQuery> _queryValidator;
        private readonly IMapper _mapper;

        public CategoryByIdQueryHandler(ICategoryService categoryService, IValidator<CategoryByIdQuery> queryValidator, IMapper mapper)
        {
            _categoryService = categoryService;
            _queryValidator = queryValidator;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(CategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _queryValidator.Validate(request);

            var category = await _categoryService.CategoryById(request.Id);

            return validationResult.IsValid ? _mapper.Map<CategoryDto>(category) : null;
        }
    }
}