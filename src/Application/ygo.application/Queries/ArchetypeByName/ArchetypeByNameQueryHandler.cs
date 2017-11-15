using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.ArchetypeByName
{
    public class ArchetypeByNameQueryHandler : IAsyncRequestHandler<ArchetypeByNameQuery, ArchetypeDto>
    {
        private readonly IArchetypeRepository _repository;
        private readonly IValidator<ArchetypeByNameQuery> _validator;

        public ArchetypeByNameQueryHandler(IArchetypeRepository repository, IValidator<ArchetypeByNameQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ArchetypeDto> Handle(ArchetypeByNameQuery message)
        {
            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
            {
                var result = await _repository.ArchetypeByName(message.Name);

                return result.MapToArchetypeDto();
            }

            return null;
        }
    }
}