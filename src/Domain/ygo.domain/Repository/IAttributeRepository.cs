using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.domain.Repository
{
    public interface IAttributeRepository
    {
        Task<List<Attribute>> AllAttributes();
    }
}