﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly YgoDbContext _dbContext;

        public AttributeRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Attribute>> AllAttributes()
        {
            return _dbContext.Attribute.OrderBy(c => c.Name).ToListAsync();
        }

    }
}