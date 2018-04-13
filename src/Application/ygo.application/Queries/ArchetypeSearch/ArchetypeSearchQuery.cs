using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.ArchetypeSearch
{
    public class ArchetypeSearchQuery : IRequest<ArchetypeDto>
    {
        public string SearchTerm { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}