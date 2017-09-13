using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ygo.application.Repository;
using ygo.domain.Models;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly YgoDbContext _context;

        public CardRepository(YgoDbContext context)
        {
            _context = context;
        }

        public Task<Card> CardByName(string name)
        {
            return _context
                    .Card
                    .Include(c => c.CardSubCategory)
                    .Include(c => c.CardAttribute)
                    .Include(c => c.CardType)
                    .Include(c => c.CardLinkArrow)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Card> Add(Card newCard)
        {
            _context.Card.Add(newCard);

            await _context.SaveChangesAsync();

            return newCard;
        }

        public Task<Card> CardById(long id)
        {
            return _context
                        .Card
                        .Include(c => c.CardSubCategory)
                        .Include(c => c.CardAttribute)
                        .Include(c => c.CardType)
                        .Include(c => c.CardLinkArrow)
                        .AsNoTracking()
                        .SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Card> Update(Card card)
        {
            _context.Card.Update(card);

            await _context.SaveChangesAsync();

            return card;
        }

        public Task<bool> CardExists(long id)
        {
            return _context.Card.AnyAsync(c => c.Id == id);
        }
    }
}