using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.domain.Models;

namespace ygo.application.Repository
{
    public interface ITypeRepository
    {
        Task<List<Type>> AllTypes();
    }
}