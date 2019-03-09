using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.AllLimits
{
    public class AllLimitsQueryHandler : IRequestHandler<AllLimitsQuery, IEnumerable<LimitDto>>
    {
        private readonly ILimitService _limitService;

        public AllLimitsQueryHandler(ILimitService limitService)
        {
            _limitService = limitService;
        }

        public async Task<IEnumerable<LimitDto>> Handle(AllLimitsQuery request, CancellationToken cancellationToken)
        {
            var allLimits = await _limitService.AllLimits();

            return Mapper.Map<IEnumerable<LimitDto>>(allLimits);
        }
    }
}