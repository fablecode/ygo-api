using System.Collections.Generic;
using MediatR;
using ygo.domain.Models;

namespace ygo.application.Query.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
    {
        
    }
}