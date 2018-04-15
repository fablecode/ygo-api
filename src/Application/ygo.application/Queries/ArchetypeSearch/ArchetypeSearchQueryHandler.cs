using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.application.Paging;
using ygo.domain.Repository;

namespace ygo.application.Queries.ArchetypeSearch
{
    public class ArchetypeSearchQueryHandler : IRequestHandler<ArchetypeSearchQuery, PagedList<ArchetypeDto>>
    {
        private readonly IArchetypeRepository _archetypeRepository;

        public ArchetypeSearchQueryHandler(IArchetypeRepository archetypeRepository)
        {
            _archetypeRepository = archetypeRepository;
        }

        public async Task<PagedList<ArchetypeDto>> Handle(ArchetypeSearchQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await _archetypeRepository.Search(request.SearchTerm, request.PageNumber, request.PageSize);

            var archetypeList = searchResult.Items.Select(a => a.MapToArchetypeDto()).ToList();

            return new PagedList<ArchetypeDto>(archetypeList, searchResult.TotalRecords, request.PageNumber,request.PageSize);
        }
    }
}