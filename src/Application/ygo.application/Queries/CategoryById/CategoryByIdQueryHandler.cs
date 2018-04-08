using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ygo.domain.Repository;
using ygo.infrastructure.Models;

namespace ygo.application.Queries.CategoryById
{
    public class CategoryByIdQueryHandler: IRequestHandler<CategoryByIdQuery, Category>
    {
        private readonly ICategoryRepository _repository;
        private readonly IValidator<CategoryByIdQuery> _queryValidator;

        public CategoryByIdQueryHandler(ICategoryRepository repository, IValidator<CategoryByIdQuery> queryValidator)
        {
            _repository = repository;
            _queryValidator = queryValidator;
        }

        public Task<Category> Handle(CategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _queryValidator.Validate(request);

            return validationResult.IsValid ? _repository.CategoryById(request.Id) : null;
        }
    }
}