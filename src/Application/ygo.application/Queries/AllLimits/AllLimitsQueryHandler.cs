using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.AllLimits
{
    public class AllLimitsQueryHandler : IAsyncRequestHandler<AllLimitsQuery, IEnumerable<LimitDto>>
    {
        private readonly ILimitRepository _repository;

        public AllLimitsQueryHandler(ILimitRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LimitDto>> Handle(AllLimitsQuery message)
        {
            var allLimits = await _repository.AllLimits();

            return Mapper.Map<IEnumerable<LimitDto>>(allLimits);
        }
    }
}