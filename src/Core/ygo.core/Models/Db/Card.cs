using System;
using System.Collections.Generic;

namespace ygo.core.Models.Db
{
    public class Card
    {
        public Card()
        {
            ArchetypeCard = new HashSet<ArchetypeCard>();
            BanlistCard = new HashSet<BanlistCard>();
            CardAttribute = new HashSet<CardAttribute>();
            CardLinkArrow = new HashSet<CardLinkArrow>();
            CardRuling = new HashSet<CardRuling>();
            CardSubCategory = new HashSet<CardSubCategory>();
            CardTrivia = new HashSet<CardTrivia>();
            CardType = new HashSet<CardType>();
            RulingSection = new HashSet<RulingSection>();
            TipSection = new HashSet<TipSection>();
            TriviaSection = new HashSet<TriviaSection>();
        }

        public long Id { get; set; }
        public long? CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ICollection<ArchetypeCard> ArchetypeCard { get; set; }
        public ICollection<BanlistCard> BanlistCard { get; set; }
        public ICollection<CardAttribute> CardAttribute { get; set; }
        public ICollection<CardLinkArrow> CardLinkArrow { get; set; }
        public ICollection<CardRuling> CardRuling { get; set; }
        public ICollection<CardSubCategory> CardSubCategory { get; set; }
        public ICollection<CardTrivia> CardTrivia { get; set; }
        public ICollection<CardType> CardType { get; set; }
        public ICollection<RulingSection> RulingSection { get; set; }
        public ICollection<TipSection> TipSection { get; set; }
        public ICollection<TriviaSection> TriviaSection { get; set; }
    }
}