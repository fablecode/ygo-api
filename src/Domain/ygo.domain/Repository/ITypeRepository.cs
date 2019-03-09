using System.Collections.Generic;
using System.Threading.Tasks;
using Type = ygo.core.Models.Db.Type;

namespace ygo.domain.Repository
{
    public interface ITypeRepository
    {
        Task<List<Type>> AllTypes();
    }
}