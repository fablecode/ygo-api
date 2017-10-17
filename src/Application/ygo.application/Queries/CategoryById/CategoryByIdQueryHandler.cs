using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.core.Models.Db;
using ygo.domain.Repository;

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

            return validationResult.IsValid ? _repository.CategoryById(message.Id) : null;
        }
    }
}