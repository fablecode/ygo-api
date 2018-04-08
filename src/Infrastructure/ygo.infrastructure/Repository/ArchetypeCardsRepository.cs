using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class ArchetypeCardsRepository : IArchetypeCardsRepository
    {
        private readonly YgoDbContext _dbContext;

        public ArchetypeCardsRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<IEnumerable<ArchetypeCard>> Update(long archetype, IEnumerable<string> cards)
        {
            throw new NotImplementedException();
        }
    }
}