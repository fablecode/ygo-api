using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.Queries.CategoryById
{
    public class CategoryByIdQueryHandler: IAsyncRequestHandler<CategoryByIdQuery, Category>
    {
        private readonly ICategoryRepository _repository;
        private readonly IValidator<CategoryByIdQuery> _queryValidator;

        public CategoryByIdQueryHandler(ICategoryRepository repository, IValidator<CategoryByIdQuery> queryValidator)
        {
            _repository = repository;
            _queryValidator = queryValidator;
        }

        public Task<Category> Handle(CategoryByIdQuery message)
        {
            var validationResult = _queryValidator.Validate(message);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            return _repository.GetCategoryById(message.Id);
        }
    }
}