using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ygo.domain.Models;

namespace ygo.infrastructure.Database
{
    public interface IYgoDbContext
    {
        DbSet<Category> Categories { get; set; }
        DatabaseFacade Database { get; }
        int SaveChanges();
    }
}