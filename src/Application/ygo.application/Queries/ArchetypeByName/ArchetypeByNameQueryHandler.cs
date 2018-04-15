using System.Threading;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.application.Queries.ArchetypeImageById;
using ygo.domain.Repository;

namespace ygo.application.Queries.ArchetypeByName
{
    public class ArchetypeByNameQueryHandler : IRequestHandler<ArchetypeByNameQuery, ArchetypeDto>
    {
        private readonly IArchetypeRepository _repository;
        private readonly IValidator<ArchetypeByNameQuery> _validator;

        public ArchetypeByNameQueryHandler(IArchetypeRepository repository, IValidator<ArchetypeByNameQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ArchetypeDto> Handle(ArchetypeByNameQuery request, CancellationToken cancellationToken)
        {
            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                var result = await _repository.ArchetypeByName(request.Name);

                return result.MapToArchetypeDto();
            }

            return null;
        }
    }
}