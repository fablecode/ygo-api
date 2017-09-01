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
            return _context.Card.SingleOrDefaultAsync(c => c.Name == name);
        }

        public Task<Card> Add(Card newCard)
        {
            throw new System.NotImplementedException();
        }
    }
}