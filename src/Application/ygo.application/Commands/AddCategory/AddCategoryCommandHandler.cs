using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.Commands.AddCategory
{
    public class AddCategoryCommandHandler : IAsyncRequestHandler<AddCategoryCommand, Category>
    {
        private readonly ICategoryRepository _repository;
        private readonly IValidator<AddCategoryCommand> _validator;

        public AddCategoryCommandHandler(ICategoryRepository repository, IValidator<AddCategoryCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public Task<Category> Handle(AddCategoryCommand message)
        {
            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
                return _repository.Add(new Category {Name = message.Name, Created = DateTime.UtcNow, Updated = DateTime.UtcNow});

            throw new ValidationException(validationResults.Errors);
        }
    }
}