using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.domain.Repository;
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
                        .ThenInclude(sc => sc.SubCategory)
                    .Include(c => c.CardAttribute)
                        .ThenInclude(ca => ca.Attribute)
                    .Include(c => c.CardType)
                        .ThenInclude(ct => ct.Type)
                    .Include(c => c.CardLinkArrow)
                        .ThenInclude(cla => cla.LinkArrow)
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
                            .ThenInclude(sc => sc.SubCategory)
                        .Include(c => c.CardAttribute)
                            .ThenInclude(ca => ca.Attribute)
                        .Include(c => c.CardType)
                            .ThenInclude(ct => ct.Type)
                        .Include(c => c.CardLinkArrow)
                            .ThenInclude(cla => cla.LinkArrow)
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

        public async Task<SearchResult<Card>> Search(string searchTerm, int pageIndex, int pageSize)
        {
            var searchResults = new SearchResult<Card>();

            var query = _context
                .Card
                .Include(c => c.CardSubCategory)
                .ThenInclude(sc => sc.SubCategory)
                .Include(c => c.CardAttribute)
                .ThenInclude(ca => ca.Attribute)
                .Include(c => c.CardType)
                .ThenInclude(ct => ct.Type)
                .Include(c => c.CardLinkArrow)
                .ThenInclude(cla => cla.LinkArrow)
                .Select(c => c);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a => EF.Functions.Like(a.Name, $"%{searchTerm}%"));
            }

            query = query
                    .AsNoTracking()
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize);

            searchResults.Items = await query.ToListAsync();
            searchResults.TotalRecords = await _context.Card.CountAsync();

            return searchResults;
        }
    }
}