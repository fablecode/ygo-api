using System.Collections.Generic;

namespace ygo.core.Models
{
    public class MonsterCardModel
    {
        public long? CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
        public int AttributeId { get; set; }
        public List<int> SubCategoryIds { get; set; } = new List<int>();
        public List<int> TypeIds { get; set; } = new List<int>();
        public List<int> LinkArrowIds { get; set; } = new List<int>();
    }
}