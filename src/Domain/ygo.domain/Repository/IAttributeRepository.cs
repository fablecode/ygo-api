using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Attribute = ygo.core.Models.Db.Attribute;

namespace ygo.domain.Repository
{
    public interface IAttributeRepository
    {
        Task<List<Attribute>> AllAttributes();
    }
}