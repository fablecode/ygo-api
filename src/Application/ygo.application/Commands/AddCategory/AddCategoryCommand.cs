using MediatR;

namespace ygo.application.Commands.AddCategory
{
    public class AddCategoryCommand : IRequest<CommandResult>
    {
        public string Name { get; set; }
    }
}