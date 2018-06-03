using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class CardRulingRepository : ICardRulingRepository
    {
        private readonly YgoDbContext _context;

        public CardRulingRepository(YgoDbContext context)
        {
            _context = context;
        }

        public Task<List<RulingSection>> RulingSectionsByCardId(long cardId)
        {
            return _context
                .RulingSection
                    .Include(t => t.Card)
                    .Include(t => t.Ruling)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeleteByCardId(long cardId)
        {
            var rulingSections = await _context
                                .RulingSection
                                .Include(t => t.Card)
                                .Include(t => t.Ruling)
                                .Where(ts => ts.CardId == cardId)
                                .ToListAsync();

            if (rulingSections.Any())
            {
                _context.Ruling.RemoveRange(rulingSections.SelectMany(t => t.Ruling));
                _context.RulingSection.RemoveRange(rulingSections);

                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(List<RulingSection> rulingSections)
        {
            _context.RulingSection.UpdateRange(rulingSections);
            await _context.SaveChangesAsync();
        }
    }
}