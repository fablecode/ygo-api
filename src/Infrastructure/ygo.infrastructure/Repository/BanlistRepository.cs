using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class BanlistRepository : IBanlistRepository
    {
        private readonly YgoDbContext _context;

        public BanlistRepository(YgoDbContext context)
        {
            _context = context;
        }

        public Task<Banlist> GetBanlistById(long id)
        {
            return _context
                    .Banlist
                    .Include(b => b.Format)
                    .Include(b => b.BanlistCard)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Banlist> Add(Banlist newBanlist)
        {
            newBanlist.Created = 
            newBanlist.Updated = DateTime.UtcNow;

            _context.Banlist.Add(newBanlist);

            await _context.SaveChangesAsync();

            return newBanlist;
        }

        public async Task<Banlist> Update(Banlist banlist)
        {
            banlist.Updated = DateTime.UtcNow;

            _context.Banlist.Update(banlist);

            await _context.SaveChangesAsync();

            return banlist;
        }

        public Task<bool> BanlistExist(long id)
        {
            return _context.Banlist.AnyAsync(b => b.Id == id);
        }
    }
}