using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface IBanlistCardsRepository
    {
        Task<ICollection<BanlistCard>> Update(long banlistId, BanlistCard[] banlistCards);
    }
}