using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class CardTriviaRepository : ICardTriviaRepository
    {
        private readonly YgoDbContext _context;

        public CardTriviaRepository(YgoDbContext context)
        {
            _context = context;
        }

        public Task<List<TriviaSection>> TriviaSectionsByCardId(long cardId)
        {
            return _context
                .TriviaSection
                    .Include(t => t.Card)
                    .Include(t => t.Trivia)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeleteByCardId(long cardId)
        {
            var triviaSections = await _context
                                .TriviaSection
                                .Include(t => t.Card)
                                .Include(t => t.Trivia)
                                .Where(ts => ts.CardId == cardId)
                                .ToListAsync();

            if (triviaSections.Any())
            {
                _context.Trivia.RemoveRange(triviaSections.SelectMany(t => t.Trivia));
                _context.TriviaSection.RemoveRange(triviaSections);

                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(List<TriviaSection> tipSections)
        {
            _context.TriviaSection.UpdateRange(tipSections);
            await _context.SaveChangesAsync();
        }
    }
}