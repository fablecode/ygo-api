using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ygo.application.Dto;
using ygo.application.Ioc;
using ygo.application.Repository;

namespace ygo.application.Queries.AllTypes
{
    public class AllTypesQueryHandler : IAsyncRequestHandler<AllTypesQuery, IEnumerable<TypeDto>>
    {
        private readonly ITypeRepository _repository;

        public AllTypesQueryHandler(ITypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TypeDto>> Handle(AllTypesQuery message)
        {
            var types = await _repository.AllTypes();

            return Mapper.Map<IEnumerable<TypeDto>>(types);
        }
    }
}