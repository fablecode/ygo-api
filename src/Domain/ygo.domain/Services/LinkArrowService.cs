using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.domain.Repository;

namespace ygo.domain.Services
{
    public class LinkArrowService : ILinkArrowService
    {
        private readonly ILinkArrowRepository _linkArrowRepository;

        public LinkArrowService(ILinkArrowRepository linkArrowRepository)
        {
            _linkArrowRepository = linkArrowRepository;
        }
        public Task<List<LinkArrow>> AllLinkArrows()
        {
            return _linkArrowRepository.AllLinkArrows();
        }
    }
}