using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.AllAttributes
{
    public class AllAttributesQueryHandler : IRequestHandler<AllAttributesQuery, IEnumerable<AttributeDto>>
    {
        private readonly IAttributeService _attributeService;
        private readonly IMapper _mapper;

        public AllAttributesQueryHandler(IAttributeService attributeService, IMapper mapper)
        {
            _attributeService = attributeService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AttributeDto>> Handle(AllAttributesQuery request, CancellationToken cancellationToken)
        {
            var allAttributes = await _attributeService.AllAttributes();

            return _mapper.Map<IEnumerable<AttributeDto>>(allAttributes);
        }
    }
}