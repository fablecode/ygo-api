using System.Threading.Tasks;
using ygo.infrastructure.Models;

namespace ygo.domain.Repository
{
    public interface IBanlistRepository
    {
        Task<Banlist> GetBanlistById(long id);
        Task<Banlist> Add(Banlist newBanlist);
        Task<Banlist> Update(Banlist banlist);
        Task<bool> BanlistExist(long id);
        Task<Banlist> GetBanlistByFormatAcronym(string acronym);
    }
}