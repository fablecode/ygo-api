using MediatR;
using ygo.domain.Models;

namespace ygo.application.Commands.AddCategory
{
    public class AddCategoryCommand : IRequest<Category>
    {
        public string Name { get; set; }
    }
}