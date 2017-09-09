using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ygo.application.Queries.CardSearch
{
    public class CardSearchQuery
    {
        public long BanlistId { get; set; }
        public long LimitId { get; set; }
        public long CategoryId { get; set; }
        public IEnumerable<long> SubCategoryIds { get; set; }
        public long AttributeId { get; set; }
        public long TypeId { get; set; }
        public int LvlRank { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public string SearchText { get; set; }

        [Range(1, 10)]
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;
    }
}