using System.Collections.Generic;
using MediatR;
using ygo.domain.Models;

namespace ygo.application.Queries.AllCategories
{
    public class AllCategoriesQuery : IRequest<IEnumerable<Category>>
    {
        
    }
}