using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;

namespace ygo.application.Queries.CategoryById
{
    public class CategoryByIdQueryHandler: IRequestHandler<CategoryByIdQuery, Category>
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoryByIdQuery> _queryValidator;

        public CategoryByIdQueryHandler(ICategoryService categoryService, IValidator<CategoryByIdQuery> queryValidator)
        {
            _categoryService = categoryService;
            _queryValidator = queryValidator;
        }

        public async Task<Category> Handle(CategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _queryValidator.Validate(request);

            return validationResult.IsValid ? await _categoryService.CategoryById(request.Id) : null;
        }
    }
}