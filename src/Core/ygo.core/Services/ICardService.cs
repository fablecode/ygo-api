using System.Threading.Tasks;
using ygo.core.Models;
using ygo.core.Models.Db;

namespace ygo.core.Services
{
    public interface ICardService
    {
        Task<Card> Add(CardModel cardModel);
        Task<Card> Update(CardModel cardModel);
        Task<Card> CardById(long cardId);
        Task<Card> CardByName(string name);
        Task<bool> CardExists(long id);
        Task<SearchResult<Card>> Search(string searchTerm, int pageIndex, int pageSize);
    }
}