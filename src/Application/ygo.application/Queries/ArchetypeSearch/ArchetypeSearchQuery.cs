using System.ComponentModel.DataAnnotations;
using MediatR;

namespace ygo.application.Queries.ArchetypeSearch
{
    public class ArchetypeSearchQuery : IRequest<QueryResult>
    {
        public string SearchTerm { get; set; } = string.Empty;
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int PageSize { get; set; }
    }
}