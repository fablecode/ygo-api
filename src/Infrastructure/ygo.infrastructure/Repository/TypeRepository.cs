using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo.domain.Repository;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class TypeRepository : ITypeRepository
    {
        private readonly YgoDbContext _dbContext;

        public TypeRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Type>> AllTypes()
        {
            throw new NotImplementedException();
        }
    }
}