using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.domain.Models;

namespace ygo.application.Repository
{
    public interface ILinkArrowRepository
    {
        Task<List<LinkArrow>> AllLinkArrows();
    }
}