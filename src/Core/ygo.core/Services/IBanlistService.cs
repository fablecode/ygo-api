using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.core.Services
{
    public interface IBanlistService
    {
        Task<Banlist> GetBanlistById(long id);
        Task<Banlist> Add(Banlist newBanlist);
        Task<Banlist> Update(Banlist banlist);
        Task<bool> BanlistExist(long id);
        Task<Banlist> GetBanlistByFormatAcronym(string acronym);
    }
}