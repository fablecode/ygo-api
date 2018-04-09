using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ICollection<BanlistCard>> Update(long banlistId, BanlistCard[] banlistCards)
        {
            var existingBanlistCards = _context.BanlistCard.Select(bl => bl).Where(bl => bl.BanlistId == banlistId).ToList();

            if (existingBanlistCards.Any())
            {
                _context.BanlistCard.RemoveRange(existingBanlistCards);
                await _context.SaveChangesAsync();
            }

            await _context.BanlistCard.AddRangeAsync(banlistCards);
            await _context.SaveChangesAsync();
            return banlistCards;
        }
    }
}