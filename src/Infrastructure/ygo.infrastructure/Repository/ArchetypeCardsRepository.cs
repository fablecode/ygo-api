using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ygo.domain.Repository;
using ygo.infrastructure.Database;
using ygo.infrastructure.Models;

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

            //var parameters = new List<SqlParameter>();

            //parameters.Add(new SqlParameter("@ArchetypeId", archetypeId));

            //var sqlRecords = new SqlParameter
            //(
            //    "@ArchetypeCards",
            //    cards.Select(c => new ArcheytpeCardsByNameRecord
            //    {
            //        ArchetypeId = archetypeId, CardName = c
            //    })
            //)
            //    {
            //        SqlDbType = SqlDbType.Structured
            //    };

            //parameters.Add(sqlRecords);

            var archetypeIdParameter = new SqlParameter("@ArchetypeId", archetypeId);
            var cardsParameter = tvp.CreateParameter("@TvpArchetypeCards");

            return await _dbContext
                        .ArchetypeCard
                        .FromSql($"EXEC usp_AddCardsToArchetype @ArchetypeId, @TvpArchetypeCards", archetypeIdParameter, cardsParameter)
                        .ToListAsync();
        }
    }

    public class TableValuedParameterBuilder
    {
        readonly string _typeName;
        readonly SqlMetaData[] _columns;
        readonly List<SqlDataRecord> _rows;

        public TableValuedParameterBuilder(string typeName, params SqlMetaData[] columns)
        {
            _typeName = typeName;
            _columns = columns;
            _rows = new List<SqlDataRecord>();
        }

        public TableValuedParameterBuilder AddRow(params object[] fieldValues)
        {
            var row = new SqlDataRecord(_columns);
            row.SetValues(fieldValues);
            _rows.Add(row);
            return this;
        }

        public SqlParameter CreateParameter(string name)
        {
            return new SqlParameter
            {
                ParameterName = name,
                Value = _rows,
                TypeName = _typeName,
                SqlDbType = SqlDbType.Structured
            };
        }
    }
}