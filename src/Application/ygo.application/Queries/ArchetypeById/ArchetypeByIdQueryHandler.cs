using MediatR;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.ArchetypeById
{
    public class ArchetypeByIdQueryHandler : IAsyncRequestHandler<ArchetypeByIdQuery, ArchetypeDto>
    {
        private readonly IArchetypeRepository _repository;

        public ArchetypeByIdQueryHandler(IArchetypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ArchetypeDto> Handle(ArchetypeByIdQuery message)
        {
            var result = await _repository.ArchetypeById(message.Id);

            return result.MapToArchetypeDto();
        }
    }
}