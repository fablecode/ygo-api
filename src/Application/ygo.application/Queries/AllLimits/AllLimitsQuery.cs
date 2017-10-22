using System.Collections.Generic;
using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.AllLimits
{
    public class AllLimitsQuery : IRequest<IEnumerable<LimitDto>>
    {
        
    }
}