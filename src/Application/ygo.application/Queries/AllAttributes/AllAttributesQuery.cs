using System.Collections.Generic;
using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.AllAttributes
{
    public class AllAttributesQuery : IRequest<IEnumerable<AttributeDto>>
    {
        
    }
}