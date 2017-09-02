using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.Queries.AllTypes
{
    public class AllTypesQueryHandler : IAsyncRequestHandler<AllTypesQuery, IEnumerable<Type>>
    {
        private readonly ITypeRepository _repository;

        public AllTypesQueryHandler(ITypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Type>> Handle(AllTypesQuery message)
        {
            return await _repository.AllTypes();
        }
    }
}