using MediatR;
using ygo.application.Dto;
using ygo.application.Paging;

namespace ygo.application.Queries.ArchetypeSearch
{
    public class ArchetypeSearchQuery : IRequest<PagedList<ArchetypeDto>>
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}