using MediatR;

namespace ygo.application.Queries.ArchetypeSearch
{
    public class ArchetypeSearchQuery : IRequest<QueryResult>
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}