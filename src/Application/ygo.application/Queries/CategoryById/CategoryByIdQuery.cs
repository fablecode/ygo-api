using MediatR;
using ygo.application.Dto;
using ygo.core.Models.Db;

namespace ygo.application.Queries.CategoryById
{
    public class CategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }
}