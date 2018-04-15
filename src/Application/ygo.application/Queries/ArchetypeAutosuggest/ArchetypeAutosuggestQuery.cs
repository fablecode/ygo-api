using System.Collections.Generic;
using MediatR;

namespace ygo.application.Queries.ArchetypeAutosuggest
{
    public class ArchetypeAutosuggestQuery : IRequest<IEnumerable<string>>
    {
        public string Filter { get; set; }
    }
}