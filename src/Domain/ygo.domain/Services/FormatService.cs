using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class FormatService : IFormatService
    {
        private readonly IFormatRepository _formatRepository;

        public FormatService(IFormatRepository formatRepository)
        {
            _formatRepository = formatRepository;
        }
        public Task<Format> FormatByAcronym(string acronym)
        {
            return _formatRepository.FormatByAcronym(acronym);
        }
    }
}