using System.Threading.Tasks;
using ygo.core.Enums;
using ygo.core.Models;
using ygo.core.Models.Db;

namespace ygo.core.Services
{
    public interface ICardTypeStrategy
    {
        Task<Card> Add(CardModel cardModel);
        Task<Card> Update(CardModel cardModel);

        bool Handles(YugiohCardType yugiohCardType);
    }
}