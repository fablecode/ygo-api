using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class BanlistService : IBanlistService
    {
        private readonly IBanlistRepository _banlistRepository;

        public BanlistService(IBanlistRepository banlistRepository)
        {
            _banlistRepository = banlistRepository;
        }
        public Task<Banlist> GetBanlistById(long id)
        {
            return _banlistRepository.GetBanlistById(id);
        }

        public Task<Banlist> Add(Banlist newBanlist)
        {
            return _banlistRepository.Add(newBanlist);
        }

        public Task<Banlist> Update(Banlist banlist)
        {
            return _banlistRepository.Update(banlist);
        }

        public Task<bool> BanlistExist(long id)
        {
            return _banlistRepository.BanlistExist(id);
        }

        public Task<Banlist> GetBanlistByFormatAcronym(string acronym)
        {
            return _banlistRepository.GetBanlistByFormatAcronym(acronym);
        }
    }
}