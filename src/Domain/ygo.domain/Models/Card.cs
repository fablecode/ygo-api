using System;
using System.Collections.Generic;

namespace ygo.domain.Models
{
    public class Card
    {
        public long Id { get; set; }

        public string CardNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? CardLevel { get; set; }

        public int? CardRank { get; set; }

        public int? Atk { get; set; }

        public int? Def { get; set; }

        public string Tips { get; set; }

        public string Rulings { get; set; }

        public string Trivia { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public virtual ICollection<BanlistCard> BanlistCards { get; set; }

        public virtual ICollection<Archetype> Archetypes { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }

        public virtual ICollection<Type> Types { get; set; }
    }
}