using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.domain.Models;

namespace ygo.application.Repository
{
    public interface IAttributeRepository
    {
        Task<List<Attribute>> AllAttributes();
    }
}