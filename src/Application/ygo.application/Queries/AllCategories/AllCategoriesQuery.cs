using System.Collections.Generic;
using MediatR;
using ygo.application.Ioc;

namespace ygo.application.Queries.AllCategories
{
    public class AllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
        
    }
}