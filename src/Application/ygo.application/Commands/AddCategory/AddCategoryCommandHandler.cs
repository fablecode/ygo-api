using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.Commands.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, CommandResult>
    {
        private readonly ICategoryRepository _repository;
        private readonly IValidator<AddCategoryCommand> _validator;

        public AddCategoryCommandHandler(ICategoryRepository repository, IValidator<AddCategoryCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                var responseData = await _repository.Add(new Category
                {
                    Name = request.Name,
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