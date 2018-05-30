using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class CardTipRepository : ICardTipRepository
    {
        private readonly YgoDbContext _context;

        public CardTipRepository(YgoDbContext context)
        {
            _context = context;
        }

        public Task<List<TipSection>> TipSectionsByCardId(long cardId)
        {
            return _context
                .TipSection
                    .Include(t => t.Card)
                    .Include(t => t.Tip)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}