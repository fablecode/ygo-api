using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ygo.api.ServiceExtensions
{
    public static class ModelStateServiceExtensions
    {
        public static IEnumerable<string> Errors(this ModelStateDictionary modelState)
        {
            var allErrors = modelState.Values.SelectMany(v => v.Errors);

            return allErrors.Select(e => e.ErrorMessage);
        }
    }
}