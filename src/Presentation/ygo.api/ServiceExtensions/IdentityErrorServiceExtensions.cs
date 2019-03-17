using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ygo.api.ServiceExtensions
{
    public static class IdentityErrorServiceExtensions
    {
        public static IEnumerable<string> Descriptions(this IEnumerable<IdentityError> identityErrors)
        {
            return identityErrors.Select(e => e.Description);
        }
    }
}