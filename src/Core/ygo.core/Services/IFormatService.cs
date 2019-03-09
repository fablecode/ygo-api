using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.core.Services
{
    public interface IFormatService
    {
        Task<Format> FormatByAcronym(string acronym);
    }
}