using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class BanlistCardsRepository : IBanlistCardsRepository
    {
        private readonly YgoDbContext _context;

        public BanlistCardsRepository(YgoDbContext context)
        {
            _context = context;
        }

        public async Task<int> Update(long banlistId , BanlistCard[] banlistCards)
        {
            var banlist = await _context.Banlist.SingleAsync(bl => bl.Id == banlistId);

            banlist.BanlistCard.Clear();
            await _context.SaveChangesAsync();

            foreach (var card in banlistCards)
                banlist.BanlistCard.Add(card);

            return await _context.SaveChangesAsync();
        }
    }
}