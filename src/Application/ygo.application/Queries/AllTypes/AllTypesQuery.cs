using System.Collections.Generic;
using MediatR;
using ygo.domain.Models;

namespace ygo.application.Queries.AllTypes
{
    public class AllTypesQuery : IRequest<IEnumerable<Type>>
    {
        
    }
}