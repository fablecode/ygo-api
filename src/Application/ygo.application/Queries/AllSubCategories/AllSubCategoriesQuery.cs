using System.Collections.Generic;
using MediatR;
using ygo.application.Dto;
using ygo.application.Ioc;

namespace ygo.application.Queries.AllSubCategories
{
    public class AllSubCategoriesQuery : IRequest<IEnumerable<SubCategoryDto>>
    {
        
    }
}