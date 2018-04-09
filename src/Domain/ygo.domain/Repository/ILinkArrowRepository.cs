using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface ILinkArrowRepository
    {
        Task<List<LinkArrow>> AllLinkArrows();
    }
}