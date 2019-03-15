using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.AllTypes
{
    public class AllTypesQueryHandler : IRequestHandler<AllTypesQuery, IEnumerable<TypeDto>>
    {
        private readonly ITypeService _typeService;
        private readonly IMapper _mapper;

        public AllTypesQueryHandler(ITypeService typeService, IMapper mapper)
        {
            _typeService = typeService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TypeDto>> Handle(AllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _typeService.AllTypes();

            return _mapper.Map<IEnumerable<TypeDto>>(types);
        }
    }
}