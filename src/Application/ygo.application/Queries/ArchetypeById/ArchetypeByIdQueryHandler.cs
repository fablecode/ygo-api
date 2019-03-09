using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.ArchetypeById
{
    public class ArchetypeByIdQueryHandler : IRequestHandler<ArchetypeByIdQuery, ArchetypeDto>
    {
        private readonly IArchetypeService _archetypeService;

        public ArchetypeByIdQueryHandler(IArchetypeService archetypeService)
        {
            _archetypeService = archetypeService;
        }

        public async Task<ArchetypeDto> Handle(ArchetypeByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _archetypeService.ArchetypeById(request.Id);

            return result.MapToArchetypeDto();
        }
    }
}