using FluentValidation;

namespace ygo.application.Commands.AddCategory
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(category => category.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 255);
        }
    }
}