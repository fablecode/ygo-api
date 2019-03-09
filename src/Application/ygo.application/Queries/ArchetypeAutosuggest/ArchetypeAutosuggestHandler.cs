using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Services;

namespace ygo.application.Queries.ArchetypeAutosuggest
{
    public class ArchetypeAutosuggestHandler : IRequestHandler<ArchetypeAutosuggestQuery, IEnumerable<string>>
    {
        private readonly IArchetypeService _archetypeService;

        public ArchetypeAutosuggestHandler(IArchetypeService archetypeService)
        {
            _archetypeService = archetypeService;
        }

        public async Task<IEnumerable<string>> Handle(ArchetypeAutosuggestQuery request, CancellationToken cancellationToken)
        {
            var result = await _archetypeService.Names(request.Filter);

            return result;
        }
    }
}