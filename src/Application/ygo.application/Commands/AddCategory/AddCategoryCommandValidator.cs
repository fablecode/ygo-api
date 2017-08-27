using FluentValidation;

namespace ygo.application.Commands.AddCategory
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            DefaultValidatorExtensions.NotNull(RuleFor(category => category.Name))
                .NotEmpty()
                .Length(3, 255);
        }
    }
}