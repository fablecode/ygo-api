using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.ArchetypeById
{
    public class ArchetypeByIdQuery : IRequest<ArchetypeDto>
    {
        public long Id { get; set; }
    }
}