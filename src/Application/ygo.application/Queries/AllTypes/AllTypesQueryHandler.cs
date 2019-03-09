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

        public AllTypesQueryHandler(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public async Task<IEnumerable<TypeDto>> Handle(AllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _typeService.AllTypes();

            return Mapper.Map<IEnumerable<TypeDto>>(types);
        }
    }
}