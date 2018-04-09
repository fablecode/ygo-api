using MediatR;
using ygo.core.Models.Db;

namespace ygo.application.Queries.CategoryById
{
    public class CategoryByIdQuery : IRequest<Category>
    {
        public int Id { get; set; }
    }
}