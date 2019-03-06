using System;
using System.Collections.Generic;
using ygo.core.Enums;

namespace ygo.core.Models
{
    public class CardModel
    {
        public long Id { get; set; }
        public YugiohCardType CardType { get; set; }
        public long? CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
        public Uri ImageUrl { get; set; }
        public int? AttributeId { get; set; }
        public List<int> SubCategoryIds { get; set; }
        public List<int> TypeIds { get; set; }
        public List<int> LinkArrowIds { get; set; }
    }
}