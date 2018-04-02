using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.AllAttributes
{
    public class AllAttributesQueryHandler : IRequestHandler<AllAttributesQuery, IEnumerable<AttributeDto>>
    {
        private readonly IAttributeRepository _repository;

        public AllAttributesQueryHandler(IAttributeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AttributeDto>> Handle(AllAttributesQuery request, CancellationToken cancellationToken)
        {
            var allAttributes = await _repository.AllAttributes();

            return Mapper.Map<IEnumerable<AttributeDto>>(allAttributes);
        }
    }
}