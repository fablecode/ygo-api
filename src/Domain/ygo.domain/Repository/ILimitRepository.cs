using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.infrastructure.Models;

namespace ygo.domain.Repository
{
    public interface ILimitRepository
    {
        Task<List<Limit>> AllLimits();
    }
}