using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ygo.domain.Repository
{
    public interface ITypeRepository
    {
        Task<List<Type>> AllTypes();
    }
}