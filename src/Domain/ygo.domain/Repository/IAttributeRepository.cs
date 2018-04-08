using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ygo.domain.Repository
{
    public interface IAttributeRepository
    {
        Task<List<Attribute>> AllAttributes();
    }
}