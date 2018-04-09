using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface ILimitRepository
    {
        Task<List<Limit>> AllLimits();
    }
}