﻿using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;
using ygo.infrastructure.Database.TableValueParameter;

namespace ygo.infrastructure.Repository
{
    public class ArchetypeCardsRepository : IArchetypeCardsRepository
    {
        private readonly YgoDbContext _dbContext;

        public ArchetypeCardsRepository(YgoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ArchetypeCard>> Update(long archetypeId, IEnumerable<string> cards)
        {
            var tvp = new TableValuedParameterBuilder
            (
                "tvp_ArchetypeCardsByCardName",
                new SqlMetaData("ArchetypeId", SqlDbType.BigInt), 
                new SqlMetaData("CardName", SqlDbType.NVarChar, 255)
            );

            foreach (var card in cards)
            {
                tvp.AddRow(archetypeId, card);
            }

            var archetypeIdParameter = new SqlParameter("@ArchetypeId", archetypeId);
            var cardsParameter = tvp.CreateParameter("@TvpArchetypeCards");

            return await _dbContext
                        .ArchetypeCard
                        .FromSql("EXECUTE usp_AddCardsToArchetype @ArchetypeId, @TvpArchetypeCards", archetypeIdParameter, cardsParameter)
                        .ToListAsync();
        }
    }
}