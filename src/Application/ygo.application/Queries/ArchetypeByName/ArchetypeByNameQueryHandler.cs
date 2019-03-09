using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.ArchetypeByName
{
    public class ArchetypeByNameQueryHandler : IRequestHandler<ArchetypeByNameQuery, ArchetypeDto>
    {
        private readonly IArchetypeService _archetypeService;
        private readonly IValidator<ArchetypeByNameQuery> _validator;

        public ArchetypeByNameQueryHandler(IArchetypeService archetypeService, IValidator<ArchetypeByNameQuery> validator)
        {
            _archetypeService = archetypeService;
            _validator = validator;
        }

        public async Task<ArchetypeDto> Handle(ArchetypeByNameQuery request, CancellationToken cancellationToken)
        {
            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                var result = await _archetypeService.ArchetypeByName(request.Name);

                return result.MapToArchetypeDto();
            }

            return null;
        }
    }
}