using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.Commands.AddCategory
{
    public class AddCategoryCommandHandler : IAsyncRequestHandler<AddCategoryCommand, CommandResult>
    {
        private readonly ICategoryRepository _repository;
        private readonly IValidator<AddCategoryCommand> _validator;

        public AddCategoryCommandHandler(ICategoryRepository repository, IValidator<AddCategoryCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(AddCategoryCommand message)
        {
            var response = new CommandResult();

            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
            {
                var responseData = await _repository.Add(new Category
                {
                    Name = message.Name,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                });

                response.Data = responseData;
                response.IsSuccessful = true;
            }
            else
                response.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();

            return response;
        }
    }
}