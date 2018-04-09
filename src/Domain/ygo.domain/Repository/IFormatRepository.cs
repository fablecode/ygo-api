using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface IFormatRepository
    {
        Task<Format> FormatByAcronym(string acronym);
    }
}