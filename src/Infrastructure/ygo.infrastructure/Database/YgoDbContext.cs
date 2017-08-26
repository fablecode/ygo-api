using Microsoft.EntityFrameworkCore;
using ygo.domain.Models;

namespace ygo.infrastructure.Database
{
    public class YgoDbContext : DbContext, IYgoDbContext
    {
        public YgoDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
    }
}