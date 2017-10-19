using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface IBanlistRepository
    {
        Task<Banlist> GetBanlistById(long id);
        Task<Banlist> Add(Banlist newBanlist);
        Task<Banlist> Update(Banlist banlist);
        Task<bool> BanlistExist(long id);
    }
}