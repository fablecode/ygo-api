using FluentValidation;

namespace ygo.application.Queries.CategoryById
{
    public class CategoryByIdQueryValidator : AbstractValidator<CategoryByIdQuery>
    {
        public CategoryByIdQueryValidator()
        {
            RuleFor(query => query.Id)
                .GreaterThan(0);
        }
    }
}