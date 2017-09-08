using System.Threading.Tasks;
using ygo.domain.Models;

namespace ygo.application.Repository
{
    public interface ICardRepository
    {
        Task<Card> CardByName(string name);
        Task<int> Add(Card newCard);
        Task<Card> CardById(long id);
        Task<Card> Update(Card card);
    }
}