using MediatR;
using ygo.infrastructure.Models;

namespace ygo.application.Queries.CategoryById
{
    public class CategoryByIdQuery : IRequest<Category>
    {
        public int Id { get; set; }
    }
}