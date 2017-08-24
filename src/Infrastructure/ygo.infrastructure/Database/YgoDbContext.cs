using Microsoft.EntityFrameworkCore;
using ygo.domain.Models;

namespace ygo.infrastructure.Database
{
    public class YgoDbContext : DbContext, IYgoDbContext
    {
        public DbSet<Category> Categories { get; set; }
    }
}