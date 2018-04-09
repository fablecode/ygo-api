using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ygo.core.Models.Db;

namespace ygo.infrastructure.Database
{
    public interface IYgoDbContext
    {
        DbSet<Category> Category { get; set; }
        DatabaseFacade Database { get; }
        int SaveChanges();
    }
}