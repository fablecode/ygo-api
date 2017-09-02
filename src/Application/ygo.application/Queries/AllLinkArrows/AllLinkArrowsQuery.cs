using System.Collections.Generic;
using MediatR;
using ygo.domain.Models;

namespace ygo.application.Queries.AllLinkArrows
{
    public class AllLinkArrowsQuery : IRequest<IEnumerable<LinkArrow>>
    {
        
    }
}