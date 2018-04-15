using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ygo.domain.Repository;

namespace ygo.application.Queries.ArchetypeAutosuggest
{
    public class ArchetypeAutosuggestHandler : IRequestHandler<ArchetypeAutosuggestQuery, IEnumerable<string>>
    {
        private readonly IArchetypeRepository _archetypeRepository;

        public ArchetypeAutosuggestHandler(IArchetypeRepository archetypeRepository)
        {
            _archetypeRepository = archetypeRepository;
        }

        public async Task<IEnumerable<string>> Handle(ArchetypeAutosuggestQuery request, CancellationToken cancellationToken)
        {
            var result = await _archetypeRepository.Names(request.Filter);

            return result;
        }
    }
}