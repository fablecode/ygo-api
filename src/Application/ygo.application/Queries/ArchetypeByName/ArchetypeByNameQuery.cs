using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.ArchetypeByName
{
    public class ArchetypeByNameQuery : IRequest<ArchetypeDto>
    {
        public string Name { get; set; }
    }
}