using System.Collections.Generic;
using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.AllLinkArrows
{
    public class AllLinkArrowsQuery : IRequest<IEnumerable<LinkArrowDto>>
    {
        
    }
}