using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.infrastructure.Models;

namespace ygo.domain.Repository
{
    public interface ILinkArrowRepository
    {
        Task<List<LinkArrow>> AllLinkArrows();
    }
}