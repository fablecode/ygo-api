using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class LimitService : ILimitService
    {
        private readonly ILimitRepository _limitRepository;

        public LimitService(ILimitRepository limitRepository)
        {
            _limitRepository = limitRepository;
        }
        public Task<List<Limit>> AllLimits()
        {
            return _limitRepository.AllLimits();
        }
    }
}