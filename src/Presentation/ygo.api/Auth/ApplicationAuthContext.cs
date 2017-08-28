using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ygo.api.Auth
{
    public class ApplicationAuthContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationAuthContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}