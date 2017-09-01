using System.Collections.Generic;
using MediatR;
using ygo.domain.Models;

namespace ygo.application.Queries.AllSubCategories
{
    public class AllSubCategoriesQuery : IRequest<IEnumerable<SubCategory>>
    {
        
    }
}