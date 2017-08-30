using MediatR;
using ygo.domain.Models;

namespace ygo.application.Queries.CategoryById
{
    public class CategoryByIdQuery : IRequest<Category>
    {
        public int Id { get; set; }
    }
}