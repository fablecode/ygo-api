using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;

namespace ygo.domain.Services
{
    public class LimitService : ILimitService
    {
        public Task<List<Limit>> AllLimits()
        {
            throw new System.NotImplementedException();
        }
    }
}