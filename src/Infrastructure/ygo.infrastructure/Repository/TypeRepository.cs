﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ygo.application.Repository;
using ygo.domain.Models;
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
            return _dbContext
                    .Type
                    .OrderBy(t => t.Name)
                    .ToListAsync();
        }
    }
}