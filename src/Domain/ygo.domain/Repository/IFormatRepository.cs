using System.Threading.Tasks;
using ygo.infrastructure.Models;

namespace ygo.domain.Repository
{
    public interface IFormatRepository
    {
        Task<Format> FormatByAcronym(string acronym);
    }
}