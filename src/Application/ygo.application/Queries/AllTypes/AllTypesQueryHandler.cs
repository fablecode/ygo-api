using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.AllTypes
{
    public class AllTypesQueryHandler : IRequestHandler<AllTypesQuery, IEnumerable<TypeDto>>
    {
        private readonly ITypeRepository _repository;

        public AllTypesQueryHandler(ITypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TypeDto>> Handle(AllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _repository.AllTypes();

            return Mapper.Map<IEnumerable<TypeDto>>(types);
        }
    }
}