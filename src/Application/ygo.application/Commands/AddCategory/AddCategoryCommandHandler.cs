using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ygo.application.Dto;
using ygo.core.Models.Db;
using ygo.core.Services;

namespace ygo.application.Commands.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, CommandResult>
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<AddCategoryCommand> _validator;
        private readonly IMapper _mapper;

        public AddCategoryCommandHandler(ICategoryService categoryService, IValidator<AddCategoryCommand> validator, IMapper mapper)
        {
            _categoryService = categoryService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CommandResult> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                var responseData = await _categoryService.Add(new Category
                {
                    Name = request.Name,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                });

                response.Data = _mapper.Map<CategoryDto>(responseData);
                response.IsSuccessful = true;
            }
            else
                response.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();

            return response;
        }
    }
}